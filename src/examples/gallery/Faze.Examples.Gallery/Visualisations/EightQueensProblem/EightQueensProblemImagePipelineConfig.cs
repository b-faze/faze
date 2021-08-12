using Faze.Rendering.TreeRenderers;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemImagePipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int MaxDepth { get; set; }
        public bool BlackParentMoves { get; set; }
        public bool BlackUnavailableMoves { get; set; }

        public SquareTreeRendererOptions GetRendererOptions()
        {
            return new SquareTreeRendererOptions(TreeSize, ImageSize)
            {
                BorderProportion = BorderProportion,
                MaxDepth = MaxDepth
            };
        }

        public EightQueensProblemPainterConfig GetPainterConfig()
        {
            return new EightQueensProblemPainterConfig
            {
                BlackParentMoves = BlackParentMoves,
                BlackUnavailableMoves = BlackUnavailableMoves
            };
        }
    }
}
