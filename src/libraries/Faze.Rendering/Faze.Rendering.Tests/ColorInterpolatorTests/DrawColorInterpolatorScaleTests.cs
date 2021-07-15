using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.Renderers;
using Faze.Rendering.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Xunit;

namespace Faze.Rendering.Tests.ColorInterpolatorTests
{
    public class DrawColorInterpolatorScaleTests
    {
        private const int Width = 600;
        private const int Height = 50;
        private readonly ColorScaleRenderer renderer;

        public DrawColorInterpolatorScaleTests() 
        {
            this.renderer = new ColorScaleRenderer();
        }

        [DebugOnlyFact]
        public void DrawGreyscale()
        {
            var colorInterpolator = new GreyscaleInterpolator();
            var filename = FileUtilities.GetTestOutputPath(
                nameof(DrawColorInterpolatorScaleTests), $"{nameof(DrawGreyscale)}.png");

            using (var img = renderer.Draw(colorInterpolator, Width, Height))
            {
                img.Save(filename, ImageFormat.Png);
            }
        }

        [DebugOnlyFact]
        public void DrawReverseGreyscale()
        {
            var colorInterpolator = new GreyscaleInterpolator(reverse: true);
            var filename = FileUtilities.GetTestOutputPath(
                nameof(DrawColorInterpolatorScaleTests), $"{nameof(DrawReverseGreyscale)}.png");

            using (var img = renderer.Draw(colorInterpolator, Width, Height))
            {
                img.Save(filename, ImageFormat.Png);
            }
        }

        [DebugOnlyFact]
        public void DrawLinearBlueRed()
        {
            var colorInterpolator = new LinearColorInterpolator(Color.Blue, Color.Red);
            var filename = FileUtilities.GetTestOutputPath(
                nameof(DrawColorInterpolatorScaleTests), $"{nameof(DrawLinearBlueRed)}.png");

            using (var img = renderer.Draw(colorInterpolator, Width, Height))
            {
                img.Save(filename, ImageFormat.Png);
            }
        }

        [DebugOnlyFact]
        public void DrawGold()
        {
            var colorInterpolator = new GoldInterpolator();
            var filename = FileUtilities.GetTestOutputPath(
                nameof(DrawColorInterpolatorScaleTests), $"{nameof(DrawGold)}.png");

            using (var img = renderer.Draw(colorInterpolator, Width, Height))
            {
                img.Save(filename, ImageFormat.Png);
            }
        }
    }
}
