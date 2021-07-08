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
using Faze.Core.TreeLinq;
using SkiaSharp.Views.Desktop;
using System.Linq;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Faze.Rendering.Playground
{
    public class CanvasUI
    {
        private readonly PictureBox pictureBox;
        private Tree<Color> tree;
        private SquareTreeRenderer renderer;
        private Options options;
        private SquareTreeRendererOptions rendererConfig;
        private Viewport lastViewport;
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
            this.rendererConfig = new SquareTreeRendererOptions(options.Size, Math.Min(pictureBox.Width, pictureBox.Height))
            {
                BorderProportions = options.Border,
                MinChildDrawSize = options.MinChildDrawSize,
            };
            this.renderer = new SquareTreeRenderer(rendererConfig);

            this.tree = CreateRandomPaintedSquareTree(options.Size, options.RenderDepth + 1);

            canDraw = true;
        }

        internal void Save()
        {
            Save(this.renderer.Surface, @$"../../../Saves/{options.Size}_{options.RenderDepth}_{options.Border}.png");
        }

        internal void Draw(Viewport viewport)
        {
            if (!canDraw && !HasViewportChanged(viewport))
                return;

            lastViewport = viewport;
            canDraw = false;

            rendererConfig.Viewport = viewport;
            renderer.Draw(tree);

            pictureBox.Image?.Dispose();
            using (var ms = new MemoryStream())
            {
                renderer.Save(ms);
                pictureBox.Image = Image.FromStream(ms);
            }

        }

        private bool HasViewportChanged(Viewport viewport)
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

        private static Tree<Color> CreateRandomPaintedSquareTree(int size, int depth = 0)
        {
            var rnd = new Random();
            var tree = CreateSquareTree(size, depth)
                .MapValue((v, info) =>
                {
                    var maxValue = 255;
                    var relativeChildIndex = (double)info.ChildIndex / (size * size);
                    var r = (int)(relativeChildIndex * maxValue);
                    var g = (int)(relativeChildIndex * maxValue / 2);
                    var b = (int)(maxValue - relativeChildIndex * maxValue);
                    return Color.FromArgb(r, g, b);
                });

            return new Tree<Color>(tree.Value, tree.Children);
        }

        private static Tree<int> CreateSquareTree(int size, int index, int depth = 0)
        {
            var children = Enumerable.Range(0, size * size).Select(i => CreateSquareTree(size, i, depth + 1));

            return new Tree<int>(depth, children);
        }
    }
}
