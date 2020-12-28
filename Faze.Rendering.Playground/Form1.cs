using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.TreeRenderers;
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
using Faze.Rendering.TreeLinq;

namespace Faze.Rendering.Playground
{
    public partial class Form1 : Form
    {
        private CanvasUI canvasUI;

        public Form1()
        {
            InitializeComponent();

            this.timer1.Start();
            borderTrackBarChanged(null, null);
            sizeTrackBarChanged(null, null);
            maxDepthTrackBarChanged(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            canvasUI.Draw();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            var x = (float)e.X / pictureBox.Width;
            var y = (float)e.Y / pictureBox.Height;

            //template.Select(x, y);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var x = (float)e.X / pictureBox.Width;
            var y = (float)e.Y / pictureBox.Height;

            if (e.Button == MouseButtons.Left)
            {
                //template.MoveSelected(x, y);
                return;
            }

            if (e.Button == MouseButtons.None)
            {
                //template.Hover(x, y);
            }
        }

        private void borderTrackBarChanged(object sender, EventArgs e)
        {
            borderTextBox.Text = borderTrackBar.Value.ToString();
            UpdateCanvas();
        }

        private void sizeTrackBarChanged(object sender, EventArgs e)
        {
            sizeTextBox.Text = sizeTrackBar.Value.ToString();
            UpdateCanvas();
        }

        private void maxDepthTrackBarChanged(object sender, EventArgs e)
        {
            maxDepthTextBox.Text = maxDepthTrackBar.Value.ToString();
            UpdateCanvas();
        }

        private void UpdateCanvas() 
        {
            canvasUI = CreateCanvasUI();
            canvasUI.Draw();
        }

        private CanvasUI CreateCanvasUI() 
        {
            var renderer = CreateRenderer();
            var tree = CreateGreyPaintedSquareTree(sizeTrackBar.Value, maxDepthTrackBar.Value + 1);
            return new CanvasUI(pictureBox, renderer, tree);
        }

        private IPaintedTreeRenderer CreateRenderer()
        {
            return new SquareTreeRenderer(new SquareTreeRendererOptions(sizeTrackBar.Value)
            {
                BorderProportions = (double)borderTrackBar.Value / 100
            });
        }

        private static PaintedTree CreateGreyPaintedSquareTree(int size, int maxDepth, int depth = 0)
        {
            var tree = CreateSquareTree(size, maxDepth, depth)
                .Map((v, info) => info.Depth)
                .Map(v => (int)(255 * (1 - (double)v / maxDepth)))
                .Map(v => Color.FromArgb(v, v, v));

            return new PaintedTree(tree.Value, tree.Children);
        }

        private static Tree<int> CreateSquareTree(int size, int maxDepth, int depth = 0)
        {
            if (depth == maxDepth)
                return new Tree<int>(depth);

            var children = new List<Tree<int>>();
            for (var i = 0; i < size * size; i++)
            {
                children.Add(CreateSquareTree(size, maxDepth, depth + 1));
            }

            return new Tree<int>(depth, children);
        }
    }
}
