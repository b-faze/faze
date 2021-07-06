using System.Drawing;

namespace Faze.Abstractions.Rendering
{
    public interface IColorInterpolator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">Value between 0 and 1</param>
        /// <returns>Color</returns>
        Color GetColor(double d);
    }
}
