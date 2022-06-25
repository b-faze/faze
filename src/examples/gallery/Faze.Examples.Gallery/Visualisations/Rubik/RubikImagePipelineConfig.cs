using Faze.Examples.Gallery.Interfaces;

namespace Faze.Examples.Gallery.Visualisations.Rubik
{
    public class RubikImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; } = 4;
        public int ImageSize { get; set; }
        public int? MaxDepth { get; set; }
        public float BorderProportion { get; set; }
        public float? RelativeDepthFactor { get; set; }
        public bool Normalise { get; set; }
        public RubikUnavailablePaintType UnavailablePaintType { get; set; }

        public RubikPainterConfig GetPainterConfig()
        {
            return new RubikPainterConfig
            {
                Normalise = Normalise,
                UnavailablePaintType = UnavailablePaintType
            };
        }
    }
}
