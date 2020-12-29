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

        public Form1()
        {
            InitializeComponent();

            this.canvasUI = new CanvasUI(pictureBox, GetOptions());
            this.timer1.Start();
            borderTrackBarChanged(null, null);
            sizeTrackBarChanged(null, null);
            maxDepthTrackBarChanged(null, null);

            this.settings = JsonConvert.DeserializeObject<FormSettings>(File.ReadAllText(@"../../../Resources/settings.json"));
            presetDdl.DataSource = settings.Presets;
            presetDdl.DisplayMember = nameof(OptionPreset.Name);
            presetDdl.ValueMember = nameof(OptionPreset.Options);
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

        private void button2_Click(object sender, EventArgs e)
        {
            SetOptions((Options)presetDdl.SelectedValue);
        }

        private Options GetOptions()
        {
            return new Options
            {
                Size = sizeTrackBar.Value,
                RenderDepth = maxDepthTrackBar.Value,
                Border = (double)borderTrackBar.Value / 100
            };
        }

        private void SetOptions(Options options) 
        {
            sizeTrackBar.Value = options.Size;
            maxDepthTrackBar.Value = options.RenderDepth;
            borderTrackBar.Value = (int)(options.Border * 100);
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
