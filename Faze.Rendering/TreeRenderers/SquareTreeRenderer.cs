using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Faze.Rendering.TreeRenderers
{
    public class SquareTreeRendererOptions
    {
        public SquareTreeRendererOptions(int size) 
        {
            if (size <= 0)
                throw new Exception("Size must be > 0");

            Size = size;
        }

        public int Size { get; set; }
        public float BorderProportions { get; set; }
    }

    public class SquareTreeRenderer : IPaintedTreeRenderer, IDisposable
    {
        private readonly SquareTreeRendererOptions options;
        private readonly int imageSize;
        private readonly SKSurface surface;

        public SquareTreeRenderer(SquareTreeRendererOptions options, int imageSize) 
        {
            this.options = options;
            this.imageSize = imageSize;
            var imageInfo = new SKImageInfo(imageSize, imageSize);
            this.surface = SKSurface.Create(imageInfo);
        }

        public SKSurface Surface => surface;

        public Tree<T> GetVisible<T>(Tree<T> tree, IViewport viewPort)
        {
            return tree;
        }

        public void Draw(Tree<Color> tree, IViewport viewport, int? maxDepth = null)
        {
            var viewportBorderSize = 5;
            var viewportScale = GetScale(viewport);
            var viewportSize = viewportScale * imageSize;
            var viewportRectSize = viewportSize - viewportBorderSize;
            var viewportRect = SKRect.Create(viewport.Left * imageSize, viewport.Top * imageSize, viewportRectSize, viewportRectSize);
            var viewportScaledRect = SKRect.Create(-viewportRect.Left / viewportScale, -viewportRect.Top / viewportScale, imageSize / viewportScale, imageSize / viewportScale);
            var scaledViewportRect = SKRect.Create(0, 0, viewportSize / viewportScale, viewportSize / viewportScale);

            surface.Canvas.Clear();
            DrawHelper(surface.Canvas, tree, scaledViewportRect, viewportScaledRect, 0, maxDepth);

            surface.Canvas.DrawRect(viewportRect, new SKPaint
            {
                IsStroke = true,
                StrokeWidth = viewportBorderSize,
                Color = new SKColor((uint)Color.Red.ToArgb())
            });
        }

        public Bitmap GetBitmap()
        {
            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            using MemoryStream mStream = new MemoryStream(data.ToArray());

            return new Bitmap(mStream, false);
        }

        private void DrawHelper(SKCanvas canvas, Tree<Color> node, SKRect viewportRect, SKRect rect, int depth, int? maxDepth = null)
        {
            if (node == null)
                return;

            if (maxDepth.HasValue && depth > maxDepth.Value)
                return;

            canvas.DrawRect(rect, new SKPaint() { Color = new SKColor((uint)node.Value.ToArgb()) });

            if (node.Children == null || !node.Children.Any())
                return;

            var scaledSize = rect.Width;
            var borderOffset = options.BorderProportions;
            var borderSize = scaledSize * borderOffset;
            if (borderSize < 1) borderSize = 0; // Don't render a border that is sub-pixel size
            var innerRectSize = scaledSize - 2 * borderSize;
            var innerRect = SKRect.Create(rect.Left + borderSize, rect.Top + borderSize, rect.Left + innerRectSize, rect.Top + innerRectSize);
            var childSize = innerRectSize / options.Size;

            if (childSize > 1 && childSize < innerRectSize)
            {
                var childIndex = 0;
                foreach (var child in node.Children)
                {
                    var (x, y, _) = Utilities.Flatten(new[] { childIndex++ }, options.Size);
                    var childRect = SKRect.Create(innerRect.Left + innerRectSize * x, innerRect.Top + innerRectSize * y, childSize, childSize);

                    var viewportIntersect = SKRect.Intersect(viewportRect, childRect);
                    if (viewportIntersect.IsEmpty)
                        continue;

                    DrawHelper(canvas, child, viewportIntersect, childRect, depth + 1, maxDepth);
                }
            }
        }

        private float GetScale(IViewport viewport)
        {
            return (float)(1 / Math.Pow(options.Size, viewport.Scale));
        }

        public void Dispose()
        {
            this.surface?.Dispose();
        }
    }
}
