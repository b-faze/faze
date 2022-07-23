using Faze.Examples.Gallery.Interfaces;
using System;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXDepth : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return GetVariation(4);
        }

        private GalleryItemMetadata GetVariation(int depth)
        {
            return new GalleryItemMetadata<OXDepthImagePipelineConfig>
            {
                FileId = $"V1 OX Depth {depth}.png",
                Album = Albums.OX,
                PipelineId = OXDepthImagePipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OXDepthImagePipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = (int)Math.Pow(3, depth) * 10,
                    MaxDepth = depth
                }
            };
        }
    }

}
