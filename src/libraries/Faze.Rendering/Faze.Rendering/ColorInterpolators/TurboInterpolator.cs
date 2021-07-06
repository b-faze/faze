using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Rendering.ColorInterpolators
{
    public class TurboInterpolator : IColorInterpolator
    {
        public Color GetColor(double d)
        {
            var r = (int)Math.Max(0, Math.Min(255, Math.Round(34.61 + d * (1172.33 - d * (10793.56 - d * (33300.12 - d * (38394.49 - d * 14825.05)))))));
            var g = (int)Math.Max(0, Math.Min(255, Math.Round(23.31 + d * (557.33 + d * (1225.33 - d * (3574.96 - d * (1073.77 + d * 707.56)))))));
            var b = (int)Math.Max(0, Math.Min(255, Math.Round(27.2 + d * (3211.1 - d * (15327.97 - d * (27814 - d * (22569.18 - d * 6838.66)))))));

            return Color.FromArgb(r, g, b);
        }
    }
}
