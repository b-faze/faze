using Faze.Abstractions.Rendering;
using System;
using System.Drawing;

namespace Faze.Rendering.ColorInterpolators
{
    public class GoldInterpolator : IColorInterpolator
    {
        public Color GetColor(double d)
        {
            var drawOffset = 1 - 2 * Math.Abs(0.5 - d);
            var r = (int)(255 * d + drawOffset * 127);
            var g = (int)(drawOffset * 255);
            var b = (int)(255 * (1 - d) - drawOffset * 127);

            return Color.FromArgb(255, r, g, b);
        }
    }
}
