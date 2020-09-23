using System.Drawing;

namespace Faze.Rendering.Painters
{
    public class GreyscaleInterpolator : LinearColorInterpolator, IColorInterpolator
    {
        public GreyscaleInterpolator(bool reverse = false)
            : base(reverse ? Color.White : Color.Black, reverse ? Color.Black : Color.White)
        {
        }
    }
}
