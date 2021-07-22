using System.Drawing;

namespace Faze.Abstractions.Rendering
{
    /// <summary>
    /// Intended to give a smooth transition of colour across a unit interval
    /// </summary>
    public interface IColorInterpolator
    {
        /// <param name="d">Value between 0 and 1</param>
        /// <returns>Color</returns>
        Color GetColor(double d);
    }
}
