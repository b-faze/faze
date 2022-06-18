using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;

namespace Faze.Examples.Gallery.Extensions
{
    public static class SliceAndDiceTreeRendererPipelineConfigExtensions
    {
        public static SliceAndDiceTreeRendererOptions GetRendererOptions(this ISliceAndDiceTreeRendererPipelineConfig config)
        {
            return new SliceAndDiceTreeRendererOptions(config.ImageSize)
            {
                BorderProportion = config.BorderProportion,
                MaxDepth = config.MaxDepth
            };
        }
    }
}
