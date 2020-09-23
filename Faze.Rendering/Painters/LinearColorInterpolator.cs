using Faze.Abstractions.Rendering;
using System.Drawing;

namespace Faze.Rendering.Painters
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
            var (dA, dR, dG, dB) = GetDifference();

            var newA = GetInterpolatedValue(start.A, dA, d);
            var newR = GetInterpolatedValue(start.R, dR, d);
            var newG = GetInterpolatedValue(start.G, dG, d);
            var newB = GetInterpolatedValue(start.B, dB, d);

            return Color.FromArgb(newA, newR, newG, newB);
        }

        protected (int a, int r, int g, int b) GetDifference()
        {
            return (end.A - start.A, end.R - start.R, end.G - start.G, end.B - start.B);
        }

        protected int GetInterpolatedValue(int start, int delta, double d) 
        {
            return (int)(start + delta * d);
        }
    }
}
