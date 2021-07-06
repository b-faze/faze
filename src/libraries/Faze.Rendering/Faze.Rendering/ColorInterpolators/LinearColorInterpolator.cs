using Faze.Abstractions.Rendering;
using System.Drawing;

namespace Faze.Rendering.ColorInterpolators
{
    public class LinearColorInterpolator : IColorInterpolator
    {
        private readonly Color start;
        private readonly Color end;

        public LinearColorInterpolator(Color start, Color end)
        {
            this.start = start;
            this.end = end;
        }

        public Color GetColor(double d)
        {
            var newA = GetInterpolatedValue(start.A, end.A, d);
            var newR = GetInterpolatedValue(start.R, end.R, d);
            var newG = GetInterpolatedValue(start.G, end.G, d);
            var newB = GetInterpolatedValue(start.B, end.B, d);

            return Color.FromArgb(newA, newR, newG, newB);
        }

        protected int GetInterpolatedValue(int start, int end, double d) 
        {
            return (int)(start + (end - start) * d);
        }
    }
}
