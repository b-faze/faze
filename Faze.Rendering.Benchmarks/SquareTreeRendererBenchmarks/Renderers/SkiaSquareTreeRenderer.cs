using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Microsoft.Diagnostics.Tracing.Parsers;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Faze.Rendering.TreeRenderers
{
    public class SkiaSquareTreeRenderer : IPaintedTreeRenderer
    {
        private readonly SquareTreeRendererOptions options;

        public SkiaSquareTreeRenderer(SquareTreeRendererOptions options) 
        {
            this.options = options;
        }


        public Bitmap Draw(PaintedTree tree, int size, int? maxDepth = null)
        {
            var imageInfo = new SKImageInfo(size, size);
            using SKSurface surface = SKSurface.Create(imageInfo);

            DrawHelper(surface.Canvas, tree, new SKRect(0, 0, size, size), 0, maxDepth);

            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            using MemoryStream mStream = new MemoryStream(data.ToArray());

            return new Bitmap(mStream, false);
        }

        private void DrawHelper(SKCanvas canvas, Tree<Color> node, SKRect rect, int depth, int? maxDepth = null)
        {
            if (node == null)
                return;

            if (maxDepth.HasValue && depth > maxDepth.Value)
                return;

            canvas.DrawRect(rect, new SKPaint() { Color = new SKColor((uint)node.Value.ToArgb()) });

            if (node.Children == null)
                return;

            var borderOffset = options.BorderProportions;
            var borderSize = rect.Width * borderOffset;
            var innerRectSize = rect.Width - 2 * borderSize;
            var innerRect = new SKRect(rect.Left + borderSize, rect.Top + borderSize, rect.Left + innerRectSize, rect.Top + innerRectSize);
            var childSize = innerRectSize / options.Size;

            if (childSize > 1 && childSize < innerRectSize) 
            {
                var childIndex = 0;
                foreach (var child in node.Children)
                {
                    var (x, y, _) = Utilities.Flatten(new[] { childIndex }, options.Size);
                    var childRect = new SKRect(innerRect.Left + innerRectSize * x, innerRect.Top + innerRectSize * y, childSize, childSize);
                    DrawHelper(canvas, child, childRect, depth + 1, maxDepth);

                    childIndex++;
                }
            }
        }
    }
}
