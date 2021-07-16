using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
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
        public float BorderProportion { get; set; }
        public IViewport Viewport { get; set; }
        public int? MaxDepth { get; set; }
    }

    public class SliceAndDiceTreeRenderer : IPaintedTreeRenderer
    {
        private readonly SliceAndDiceTreeRendererOptions options;

        public SliceAndDiceTreeRenderer(SliceAndDiceTreeRendererOptions options)
        {
            this.options = options;
        }

        public Tree<T> GetVisible<T>(Tree<T> tree)
        {
            throw new NotImplementedException();
        }

        public void Draw(Tree<Color> tree)
        {
            throw new NotImplementedException();
        }

        public void Save(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Bitmap Draw(Tree<Color> tree, int size, int? maxDepth = null)
        {
            var img = new Bitmap(size, size);
            Draw(img, tree, 0, maxDepth);
            return img;
        }

        public void Draw(Bitmap img, Tree<Color> node, int depth, int? maxDepth = null)
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

            var children = node.Children?.ToArray();
            if (children == null)
                return;

            var childrenCount = children.Length;
            for (var childIndex = 0; childIndex < childrenCount; childIndex++)
            {
                var child = children[childIndex];
                var borderW = (int)(img.Width * options.BorderProportion);
                var borderH = (int)(img.Height * options.BorderProportion);

                var (cx, cy, cw, ch) = GetChildRegion(childIndex, childrenCount, img.Width - borderW, img.Height - borderH);

                using (var childImg = new Bitmap(cw, ch))
                {
                    Draw(childImg, child, depth + 1, maxDepth);
                    foreach (var (pX, pY) in Utilities.GetPixels(childImg))
                    {
                        img.SetPixel(cx + borderW / 2 + pX, cy + borderH / 2 + pY, childImg.GetPixel(pX, pY));
                    }
                }
            }
        }

        private (int x, int y, int w, int h) GetChildRegion(int childIndex, int n, int w, int h)
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
