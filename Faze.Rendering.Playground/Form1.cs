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
        private readonly RecursiveTemplateUI template;

        public Form1()
        {
            InitializeComponent();

            this.template = new RecursiveTemplateUI(new RecursiveTemplate(), pictureBox);
            this.timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            template.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            template.AddChild();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            template.Draw();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            var x = (float)e.X / pictureBox.Width;
            var y = (float)e.Y / pictureBox.Height;

            template.Select(x, y);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var x = (float)e.X / pictureBox.Width;
            var y = (float)e.Y / pictureBox.Height;

            if (e.Button == MouseButtons.Left)
            {
                template.MoveSelected(x, y);
                return;
            }

            if (e.Button == MouseButtons.None)
            {
                template.Hover(x, y);
            }
        }
    }
}
