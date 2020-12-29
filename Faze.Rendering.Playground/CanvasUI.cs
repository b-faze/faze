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
        private PaintedTree tree;
        private IPaintedTreeRenderer renderer;
        private Options options;
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
            this.renderer = new SquareTreeRenderer(new SquareTreeRendererOptions(options.Size)
            {
                BorderProportions = options.Border
            });

            this.tree = CreateGreyPaintedSquareTree(options.Size, options.RenderDepth + 1);

            canDraw = true;
        }

        internal void Save()
        {
            var imageInfo = new SKImageInfo(pictureBox.Width, pictureBox.Height);
            using SKSurface surface = SKSurface.Create(imageInfo);
            var bitmap = new Bitmap(pictureBox.Image).ToSKBitmap();
            surface.Canvas.DrawBitmap(bitmap, new SKPoint(0, 0));

            Save(surface, @$"../../../Saves/{options.Size}_{options.RenderDepth}_{options.Border}.png");
        }

        internal void Draw()
        {
            if (!canDraw)
                return;

            canDraw = false;

            var width = pictureBox.Width;
            var height = pictureBox.Height;

            SKImageInfo imageInfo = new SKImageInfo(width, height);

            using SKSurface surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            var bitmap = renderer.Draw(tree, width, options.RenderDepth).ToSKBitmap();
            canvas.DrawBitmap(bitmap, new SKPoint(0, 0));

            Draw(surface);
        }

        private void Draw(SKSurface surface)
        {
            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            using MemoryStream mStream = new MemoryStream(data.ToArray());

            pictureBox.Image?.Dispose();
            pictureBox.Image = new Bitmap(mStream, false);
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

        private static PaintedTree CreateGreyPaintedSquareTree(int size, int maxDepth, int depth = 0)
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

            return new PaintedTree(tree.Value, tree.Children);
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
