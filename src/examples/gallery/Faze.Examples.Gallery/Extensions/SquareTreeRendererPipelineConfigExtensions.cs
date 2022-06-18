using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;

namespace Faze.Examples.Gallery.Extensions
{
    public static class SquareTreeRendererPipelineConfigExtensions
    {
        public static SquareTreeRendererOptions GetRendererOptions(this ISquareTreeRendererPipelineConfig config)
        {
            return new SquareTreeRendererOptions(config.TreeSize, config.ImageSize)
            {
                BorderProportion = config.BorderProportion,
                MaxDepth = config.MaxDepth,
                RelativeDepthFactor = config.RelativeDepthFactor
            };
        }
    }
}
