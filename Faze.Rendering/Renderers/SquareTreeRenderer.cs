using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System;
using System.Drawing;
using System.Linq;

namespace Faze.Rendering.Renderers
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
        public double BorderProportions { get; set; }
        public bool IgnoreRootNode { get; set; }
    }

    public class SquareTreeRenderer : IPaintedTreeRenderer
    {
        private readonly SquareTreeRendererOptions options;

        public SquareTreeRenderer(SquareTreeRendererOptions options) 
        {
            this.options = options;
        }


        public Bitmap Draw(PaintedTree tree, int size, int? maxDepth = null)
        {
            var img = new Bitmap(size, size);
            DrawHelper(img, tree, 0, maxDepth);
            return img;
        }

        private void DrawHelper(Bitmap img, Tree<Color> node, int depth, int? maxDepth = null)
        {
            if (node == null)
                return;

            if (maxDepth.HasValue && depth > maxDepth.Value)
                return;

            var color = node.Value;
            if (!color.IsEmpty)
            {
                foreach (var (pX, pY) in Utilities.GetPixels(0, 0, img.Width, img.Height))
                {
                    img.SetPixel(pX, pY, color);
                }
            }

            if (node.Children == null)
                return;

            var borderOffset = options.IgnoreRootNode && depth == 0
                ? 0
                : (int)(img.Width * options.BorderProportions);
            var innerSize = img.Width - borderOffset * 2;
            var childSize = innerSize / options.Size;
            if (childSize > 1) 
            {
                var childIndex = 0;
                foreach (var child in node.Children)
                {
                    using (var childImg = new Bitmap(childSize, childSize))
                    {
                        DrawHelper(childImg, child, depth + 1, maxDepth);

                        var (x, y, width, height) = Utilities.Flatten(new[] { childIndex }, options.Size, innerSize, innerSize);
                        foreach (var (pX, pY) in Utilities.GetPixels(childImg))
                        {
                            img.SetPixel(x + borderOffset + pX, y + borderOffset + pY, childImg.GetPixel(pX, pY));
                        }
                    }

                    childIndex++;
                }
            }
        }
    }
}
