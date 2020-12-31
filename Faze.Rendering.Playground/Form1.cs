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
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Faze.Rendering.Playground
{
    public partial class Form1 : Form
    {
        private readonly CanvasUI canvasUI;
        private FormSettings settings;

        private (float x, float y) lastMouseDragPoint;

        public Form1()
        {
            InitializeComponent();

            this.canvasUI = new CanvasUI(pictureBox, GetOptions());
            this.timer1.Start();
            borderTrackBarChanged(null, null);
            sizeTrackBarChanged(null, null);
            maxDepthTrackBarChanged(null, null);
            SetViewport(Viewport.Default());

            this.settings = JsonConvert.DeserializeObject<FormSettings>(File.ReadAllText(@"../../../Resources/settings.json"));
            presetDdl.DataSource = settings.Presets;
            presetDdl.DisplayMember = nameof(OptionPreset.Name);
            presetDdl.ValueMember = nameof(OptionPreset.Options);

            pictureBox.MouseWheel += new MouseEventHandler(this.pictureBox_MouseWheel);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            canvasUI.Draw(GetViewport());
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            var x = (float)e.X / pictureBox.Width;
            var y = (float)e.Y / pictureBox.Height;

            lastMouseDragPoint = (x, y);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var x = (float)e.X / pictureBox.Width;
            var y = (float)e.Y / pictureBox.Height;

            if (e.Button == MouseButtons.Left)
            {
                var viewport = GetViewport();

                var dx = (x - lastMouseDragPoint.x) * viewport.Scale;
                var dy = (y - lastMouseDragPoint.y) * viewport.Scale;

                
                var newViewport = viewport.Pan(-dx, -dy);
                SetViewport(newViewport);
            }

            if (e.Button == MouseButtons.None)
            {
                //template.Hover(x, y);
            }

            lastMouseDragPoint = (x, y);
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            const float wheelDeltaFactor = 0.9f;
            var viewport = GetViewport();

            var wheelScaleFactor = e.Delta / 120 * wheelDeltaFactor;
            var scaleFactor = wheelScaleFactor >= 0 ? wheelScaleFactor : 1 / -wheelScaleFactor;
            var newScale = viewport.Scale * scaleFactor;

            var x = (float)e.Location.X / pictureBox.Width;
            var y = (float)e.Location.Y / pictureBox.Height;

            var newViewport = viewport.Zoom(x, y, newScale);
            SetViewport(newViewport);
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

        private void button2_Click(object sender, EventArgs e)
        {
            var options = (Options)presetDdl.SelectedValue;
            SetOptions(options);
            SetViewport(options.DefaultViewport);
        }

        private Options GetOptions()
        {
            return new Options
            {
                Size = sizeTrackBar.Value,
                RenderDepth = maxDepthTrackBar.Value,
                Border = (float)borderTrackBar.Value / 100
            };
        }

        private void SetOptions(Options options) 
        {
            sizeTrackBar.Value = options.Size;
            maxDepthTrackBar.Value = options.RenderDepth;
            borderTrackBar.Value = (int)(options.Border * 100);
        }

        private Viewport GetViewport()
        {
            return new Viewport(float.Parse(viewportLeftTxt.Text), float.Parse(viewportTopTxt.Text), float.Parse(viewportScaleTxt.Text));
        }

        private void SetViewport(Viewport viewport)
        {
            viewportLeftTxt.Text = viewport.Left.ToString();
            viewportTopTxt.Text = viewport.Top.ToString();
            viewportScaleTxt.Text = viewport.Scale.ToString();
        }

        private void UpdateCanvas() 
        {
            canvasUI.SetOptions(GetOptions());
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            canvasUI.Save();
            MessageBox.Show("Saved");
        }
    }
}
