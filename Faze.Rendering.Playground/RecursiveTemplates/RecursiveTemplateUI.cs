using SkiaSharp;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Faze.Rendering.Playground
{
    internal class RecursiveTemplateUI
    {
        private readonly RecursiveTemplate template;
        private readonly PictureBox pictureBox;

        private RecursiveTemplateChildArea hoverChild;
        private RecursiveTemplateChildArea selectedChild;
        private (float x, float y) selectedPosOffset;

        public RecursiveTemplateUI(RecursiveTemplate template, PictureBox pictureBox)
        {
            this.template = template;
            this.pictureBox = pictureBox;
        }

        public void Select(float x, float y)
        {
            selectedChild = template.GetTopChildAt(x, y);
            if (selectedChild != null)
            {
                var (mX, mY) = selectedChild.GetMovePoint();
                selectedPosOffset = (mX - x, mY - y);
            }
        }

        public void MoveSelected(float x, float y)
        {
            if (selectedChild == null)
                return;

            var (dx, dy) = selectedPosOffset;
            selectedChild.Move(x + dx, y + dy);
        }

        public void Draw()
        {
            var width = pictureBox.Width;
            var height = pictureBox.Height;
            SKImageInfo imageInfo = new SKImageInfo(width, height);

            using SKSurface surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            foreach (var child in template.Children)
            {
                switch (child)
                {
                    case RectRecursiveTemplateChildArea rectChild:
                        using (SKPaint paint = new SKPaint())
                        {
                            paint.Style = SKPaintStyle.Stroke;
                            if (child == selectedChild || child == hoverChild)
                            {
                                paint.Color = SKColors.Red;
                            }
                            var rect = new SKRect(rectChild.Left * width, rectChild.Top * height, rectChild.Right * width, rectChild.Bottom * height);
                            canvas.DrawRect(rect, paint);
                        }
                        break;
                }
            }

            Draw(surface);
        }

        internal void Hover(float x, float y)
        {
            hoverChild = template.GetTopChildAt(x, y);

            if (selectedChild != null)
            {
                if (selectedChild.WithinResizeUI(x, y))
                {
                    Cursor.Current = selectedChild.GetResizeCursor(x, y);
                }
                
            }
        }

        internal void AddChild()
        {
            var child = new RectRecursiveTemplateChildArea(0.4f, 0.4f, 0.5f, 0.5f);
            template.AddChild(child);
        }

        internal void Clear()
        {
            template.Clear();
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
