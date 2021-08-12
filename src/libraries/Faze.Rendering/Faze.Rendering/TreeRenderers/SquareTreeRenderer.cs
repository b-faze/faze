using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Faze.Rendering.TreeRenderers
{
    public class SquareTreeRendererOptions
    {
        private const float DefaultMinChildDrawSize = 1;

        public SquareTreeRendererOptions(int treeSize, int imageSize) 
        {
            if (treeSize <= 0)
                throw new Exception("Size must be greater than 0");

            Size = treeSize;
            ImageSize = imageSize;
            MinChildDrawSize = DefaultMinChildDrawSize;
            Viewport = Abstractions.Rendering.Viewport.Default();
        }

        public int Size { get; }
        public int ImageSize { get; }
        public float BorderProportion { get; set; }
        public float MinChildDrawSize { get; set; }

        public IViewport Viewport { get; set; }
        public int? MaxDepth { get; set; }
    }

    public class SquareTreeRenderer : IPaintedTreeRenderer, IDisposable
    {
        private readonly SquareTreeRendererOptions options;
        private readonly SKSurface surface;

        public SquareTreeRenderer(SquareTreeRendererOptions options) 
        {
            this.options = options;
            var imageSize = options.ImageSize;
            var imageInfo = new SKImageInfo(imageSize, imageSize);
            this.surface = SKSurface.Create(imageInfo);
        }

        public SKSurface Surface => surface;

        public Tree<T> GetVisible<T>(Tree<T> tree)
        {
            if (options.MaxDepth != null)
                return tree.LimitDepth(options.MaxDepth.Value);

            return tree;
        }

        public void Draw(Tree<Color> tree)
        {
            var imageSize = options.ImageSize;
            var viewport = options.Viewport;
            var viewportBorderSize = 5;
            var viewportScale = viewport.Scale;
            var viewportSize = viewportScale * imageSize;
            var viewportRectSize = viewportSize - viewportBorderSize;
            var viewportRect = SKRect.Create(viewport.Left * imageSize, viewport.Top * imageSize, viewportRectSize, viewportRectSize);
            var viewportScaledRect = SKRect.Create(-viewportRect.Left / viewportScale, -viewportRect.Top / viewportScale, imageSize / viewportScale, imageSize / viewportScale);
            var scaledViewportRect = SKRect.Create(0, 0, viewportSize / viewportScale, viewportSize / viewportScale);

            surface.Canvas.Clear();
            DrawHelper(surface.Canvas, tree, scaledViewportRect, viewportScaledRect, 0, options.MaxDepth);
        }

        public void WriteToStream(Stream stream)
        {
            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);

            data.AsStream().CopyTo(stream);
        }

        private void DrawHelper(SKCanvas canvas, Tree<Color> node, SKRect viewportRect, SKRect rect, int depth, int? maxDepth = null)
        {
            if (node == null)
                return;

            if (maxDepth.HasValue && depth > maxDepth.Value)
                return;

            canvas.DrawRect(rect, new SKPaint() { Color = new SKColor((uint)node.Value.ToArgb()) });

            if (node.IsLeaf())
                return;

            var scaledSize = rect.Width;
            var borderOffset = options.BorderProportion;
            var borderSize = scaledSize * borderOffset;
            //if (borderSize < 1) borderSize = 0; // Don't render a border that is sub-pixel size
            var innerRectSize = scaledSize - 2 * borderSize;
            var innerRect = SKRect.Create(rect.Left + borderSize, rect.Top + borderSize, innerRectSize, innerRectSize);
            var childSize = innerRectSize / options.Size;

            if (childSize > options.MinChildDrawSize && childSize < innerRectSize)
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

        public void Dispose()
        {
            this.surface?.Dispose();
        }
    }
}
