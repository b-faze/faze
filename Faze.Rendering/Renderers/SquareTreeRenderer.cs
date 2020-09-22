using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System.Drawing;

namespace Faze.Rendering.Renderers
{
    public class SquareTreeRenderer : IPaintedTreeRenderer
    {
        private readonly int squareSize;
        private readonly double borderProportions;

        public SquareTreeRenderer(int squareSize, double borderProportions) 
        {
            this.squareSize = squareSize;
            this.borderProportions = borderProportions;
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

            var borderOffset = (int)(img.Width * borderProportions);
            var childSize = img.Width / squareSize - borderOffset;
            if (childSize > 1) 
            {
                var childIndex = 0;
                foreach (var child in node.Children)
                {
                    using (var childImg = new Bitmap(childSize, childSize))
                    {
                        DrawHelper(childImg, child, depth + 1, maxDepth);

                        var (x, y, width, height) = Utilities.Flatten(new[] { childIndex }, squareSize, img.Width, img.Height);
                        foreach (var (pX, pY) in Utilities.GetPixels(0, 0, childSize, childSize))
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
