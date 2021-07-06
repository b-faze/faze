using Faze.Abstractions.Rendering;
using System.Drawing;

namespace Faze.Rendering.ColorInterpolators
{
    public class GreyscaleInterpolator : LinearColorInterpolator, IColorInterpolator
    {
        public GreyscaleInterpolator(bool reverse = false)
            : base(reverse ? Color.White : Color.Black, reverse ? Color.Black : Color.White)
        {
        }
    }
}
