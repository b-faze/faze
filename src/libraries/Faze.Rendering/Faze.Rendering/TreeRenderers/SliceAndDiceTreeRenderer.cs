using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Faze.Rendering.TreeRenderers
{
    public class SliceAndDiceTreeRendererOptions
    {
        public SliceAndDiceTreeRendererOptions(int imageSize)
        {
            ImageSize = imageSize;
        }

        public int ImageSize { get; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }
    }

    public class SliceAndDiceTreeRenderer : IPaintedTreeRenderer
    {
        private readonly SliceAndDiceTreeRendererOptions options;
        private readonly SKSurface surface;

        public SliceAndDiceTreeRenderer(SliceAndDiceTreeRendererOptions options)
        {
            this.options = options;
            var imageSize = options.ImageSize;
            var imageInfo = new SKImageInfo(imageSize, imageSize);
            this.surface = SKSurface.Create(imageInfo);
        }

        public Tree<T> GetVisible<T>(Tree<T> tree)
        {
            return tree;
        }

        public void Draw(Tree<Color> tree)
        {
            surface.Canvas.Clear();

            DrawHelper(surface.Canvas, tree, SKRect.Create(0, 0, options.ImageSize, options.ImageSize), 0, options.MaxDepth);
        }

        public void Save(Stream stream)
        {
            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);

            data.AsStream().CopyTo(stream);
        }

        private void DrawHelper(SKCanvas canvas, Tree<Color> node, SKRect rect, int depth, int? maxDepth = null)
        {
            if (node == null)
                return;

            if (maxDepth.HasValue && depth > maxDepth.Value)
                return;

            var color = node.Value;
            if (!color.IsEmpty)
            {
                canvas.DrawRect(rect, new SKPaint() { Color = new SKColor((uint)node.Value.ToArgb()) });
            }

            var children = node.Children?.ToArray();
            if (children == null)
                return;

            var childrenCount = children.Length;
            var borderW = rect.Width * options.BorderProportion;
            var borderH = rect.Height * options.BorderProportion;
            var innerRect = SKRect.Create(rect.Left + borderW, rect.Top + borderH, rect.Width - borderW * 2, rect.Height - borderH * 2);

            for (var childIndex = 0; childIndex < childrenCount; childIndex++)
            {
                var child = children[childIndex];

                var (cx, cy, cw, ch) = GetChildRegion(childIndex, childrenCount, innerRect.Width, innerRect.Height);
                var childRect = SKRect.Create(innerRect.Left + cx, innerRect.Top + cy, cw, ch);

                DrawHelper(canvas, child, childRect, depth + 1, maxDepth);
            }
        }

        private (float x, float y, float w, float h) GetChildRegion(int childIndex, int n, float w, float h)
        {
            var (f1, f2) = GetFactorPairs(n)
                .OrderBy(pair => GetAspectRatio(pair.a, pair.b))
                .First();

            int cols;
            int rows;

            if (w >= h)
            {
                cols = Math.Max(f1, f2);
                rows = Math.Min(f1, f2);
            }
            else
            {
                cols = Math.Min(f1, f2);
                rows = Math.Max(f1, f2);
            }

            var cw = w / cols;
            var ch = h / rows;

            var (x, y) = Utilities.To2D(childIndex, cols);

            return (cw * x, ch * y, cw, ch);
        }

        private IEnumerable<(int a, int b)> GetFactorPairs(int n)
        {
            return Enumerable.Range(1, (int)Math.Floor(Math.Sqrt(n)))
                .Where(x => n % x == 0)
                .Select(x => (x, n / x));
        }

        private double GetAspectRatio(int a, int b)
        {
            return Math.Max(a, b) / Math.Min(a, b);
        }
    }
}
