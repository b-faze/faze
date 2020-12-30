using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.TreeRenderers;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Faze.Rendering.TreeLinq;
using SkiaSharp.Views.Desktop;
using System.Linq;
using System.Drawing.Imaging;

namespace Faze.Rendering.Playground
{
    public class CanvasUI
    {
        private readonly PictureBox pictureBox;
        private Tree<Color> tree;
        private SquareTreeRenderer renderer;
        private Options options;

        private IViewPort lastViewport;
        private bool canDraw;

        public CanvasUI(PictureBox pictureBox, Options options)
        {
            this.pictureBox = pictureBox;
            this.canDraw = true;

            SetOptions(options);
        }

        internal void SetOptions(Options options)
        {
            canDraw = false;

            this.options = options;
            this.renderer?.Dispose();
            this.renderer = new SquareTreeRenderer(new SquareTreeRendererOptions(options.Size)
            {
                BorderProportions = options.Border
            }, Math.Min(pictureBox.Width, pictureBox.Height));

            this.tree = CreateGreyPaintedSquareTree(options.Size, options.RenderDepth + 1);

            canDraw = true;
        }

        internal void Save()
        {
            Save(this.renderer.Surface, @$"../../../Saves/{options.Size}_{options.RenderDepth}_{options.Border}.png");
        }

        internal void Draw(IViewPort viewport)
        {
            if (!canDraw && !HasViewportChanged(viewport))
                return;

            lastViewport = viewport;
            canDraw = false;

            renderer.Draw(tree, viewport, options.RenderDepth);

            pictureBox.Image?.Dispose();
            pictureBox.Image = renderer.GetBitmap();
        }

        private bool HasViewportChanged(IViewPort viewport)
        {
            return lastViewport.Left != viewport.Left
                || lastViewport.Top != viewport.Top
                || lastViewport.Scale != viewport.Scale;
        }

        private void Save(SKSurface surface, string filename)
        {
            var directory = Path.GetDirectoryName(filename);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);


            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            using FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate);
            data.SaveTo(fileStream);
        }

        private static Tree<Color> CreateGreyPaintedSquareTree(int size, int maxDepth, int depth = 0)
        {
            var rnd = new Random();
            var tree = CreateSquareTree(size, maxDepth, depth)
                .Map((v, info) => info.Depth)
                .Map(v => (int)(255 * (1 - (double)v / maxDepth)))
                .Map(v =>
                {
                    var d = Math.Min(255 - v, v) / 2;
                    var dr = rnd.Next(0, d) - d / 2;
                    var dg = rnd.Next(0, d) - d / 2;
                    var db = rnd.Next(0, d) - d / 2;
                    return Color.FromArgb(v + dr, v + dg, v + db);
                });

            return new Tree<Color>(tree.Value, tree.Children);
        }

        private static Tree<int> CreateSquareTree(int size, int maxDepth, int depth = 0)
        {
            if (depth == maxDepth)
                return new Tree<int>(depth);

            var children = Enumerable.Range(0, size * size).Select(i => CreateSquareTree(size, maxDepth, depth + 1));

            return new Tree<int>(depth, children);
        }
    }
}
