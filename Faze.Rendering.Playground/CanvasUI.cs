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

namespace Faze.Rendering.Playground
{
    public class CanvasUI
    {
        private readonly PictureBox pictureBox;
        private readonly PaintedTree tree;
        private IPaintedTreeRenderer renderer;
        private bool canDraw;

        public CanvasUI(PictureBox pictureBox, IPaintedTreeRenderer renderer, PaintedTree tree)
        {
            this.pictureBox = pictureBox;
            this.renderer = renderer;

            this.tree = tree;
            this.canDraw = true;
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

            var bitmap = renderer.Draw(tree, width, 3).ToSKBitmap();
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
    }
}
