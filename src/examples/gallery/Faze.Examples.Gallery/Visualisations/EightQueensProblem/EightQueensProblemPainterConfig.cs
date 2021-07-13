using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemPainterConfig
    {
        public EightQueensProblemPainterConfig()
        {
            ColorInterpolator = new GoldInterpolator();
        }

        public IColorInterpolator ColorInterpolator { get; set; }

        /// <summary>
        /// Colors child moves black with the same index as the parent
        /// </summary>
        public bool BlackParentMoves { get; set; }
        public bool BlackUnavailableMoves { get; set; }
    }
}
