using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Faze.Rendering.Playground
{
    public partial class Form1 : Form
    {     
        public Form1()
        {
            InitializeComponent();
        }

        private void renderBtn_Click(object sender, EventArgs e)
        {
            SKImageInfo imageInfo = new SKImageInfo(300, 250);

            using (SKSurface surface = SKSurface.Create(imageInfo))
            {
                var canvas = surface.Canvas;
                canvas.DrawColor(SKColors.Red);

                using (SKImage image = surface.Snapshot())
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (MemoryStream mStream = new MemoryStream(data.ToArray()))
                {
                    Bitmap bm = new Bitmap(mStream, false);
                    pictureBox.Image = bm;
                }
            }
        }
    }
}
