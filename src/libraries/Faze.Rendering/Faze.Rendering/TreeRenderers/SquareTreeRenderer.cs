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
        public float? RelativeDepthFactor { get; set; }
    }

    public class SquareTreeRenderer : SkiaTreeRenderer, IPaintedTreeRenderer, IDisposable
    {
        private readonly SquareTreeRendererOptions options;

        public SquareTreeRenderer(SquareTreeRendererOptions options) : base(options.ImageSize)
        {
            this.options = options;
        }

        public SKSurface Surface => surface;

        public Tree<T> GetVisible<T>(Tree<T> tree)
        {
            var drawableTree = GetDrawable(tree);
            return drawableTree.MapValue(v => v.Value);
        }

        public void Draw(Tree<Color> tree)
        {
            surface.Canvas.Clear();
            var drawableTree = GetDrawable(tree);
            DrawHelper(surface.Canvas, drawableTree);
        }

        private void DrawHelper(SKCanvas canvas, Tree<Drawable<Color>> node)
        {
            if (node == null)
                return;

            canvas.DrawRect(node.Value.Rect, new SKPaint() { Color = GetSKColor(node.Value) });

            if (node.Children == null)
                return;

            foreach (var child in node.Children)
            {
                DrawHelper(canvas, child);
            }
        }

        private SKColor GetSKColor(Drawable<Color> node)
        {
            var c = node.Value;
            if (options.RelativeDepthFactor.HasValue)
            {
                var depthFactor = options.RelativeDepthFactor.Value * Math.Pow(1 - Math.Min(1, node.RelativeDepth), 2);
                c = Color.FromArgb(c.A, (int)Math.Max(0, c.R - depthFactor), (int)Math.Max(0, c.G - depthFactor), (int)Math.Max(0, c.B - depthFactor));
            }

            return new SKColor((uint)c.ToArgb());
        }
        private Tree<Drawable<T>> GetDrawable<T>(Tree<T> node)
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

            return GetDrawableHelper(node, scaledViewportRect, viewportScaledRect, 0, options.MaxDepth);
        }

        private Tree<Drawable<T>> GetDrawableHelper<T>(Tree<T> node, SKRect viewportRect, SKRect rect, int depth, int? maxDepth = null)
        {
            if (node == null)
                return null;

            if (maxDepth.HasValue && depth > maxDepth.Value)
                return null;

            // rect is outside of the viewport
            if (SKRect.Intersect(viewportRect, rect).IsEmpty)
                return null;

            var drawable = new Drawable<T>
            {
                Depth = depth,
                RelativeDepth = rect.Width / viewportRect.Width,
                Value = node.Value,
                Rect = rect
            };

            if (node.IsLeaf())
                return new Tree<Drawable<T>>(drawable);

            var scaledSize = rect.Width;
            var borderOffset = options.BorderProportion;
            var borderSize = scaledSize * borderOffset;
            //if (borderSize < 1) borderSize = 0; // Don't render a border that is sub-pixel size
            var innerRectSize = scaledSize - 2 * borderSize;
            var innerRect = SKRect.Create(rect.Left + borderSize, rect.Top + borderSize, innerRectSize, innerRectSize);
            var childSize = innerRectSize / options.Size;

            if (childSize < options.MinChildDrawSize || childSize > innerRectSize)
                return new Tree<Drawable<T>>(drawable);

            var children = node.Children.Select((child, childIndex) =>
            {
                var (x, y, _) = Utilities.Flatten(new[] { childIndex }, options.Size);
                var childRect = SKRect.Create(innerRect.Left + innerRectSize * x, innerRect.Top + innerRectSize * y, childSize, childSize);

                return GetDrawableHelper(child, viewportRect, childRect, depth + 1, maxDepth);
            });

            return new Tree<Drawable<T>>(drawable, children);
        }

        public void Dispose()
        {
            this.surface?.Dispose();
        }

        private class Drawable<T> 
        {
            public int Depth { get; set; }
            public float RelativeDepth { get; set; }
            public SKRect Rect { get; set; }
            public T Value { get; set; }
        }
    }
}
