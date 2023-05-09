using Faze.Examples.Gallery.Interfaces;
using System;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.Heart
{
    public class HeartDepth : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            for (var depth = 1; depth < 8; depth++)
            {
                yield return GetVariation(depth);
            }
        }

        private GalleryItemMetadata GetVariation(int depth)
        {
            return new GalleryItemMetadata<HeartDepthImagePipelineConfig>
            {
                FileId = $"V1 Heart Depth {depth}.png",
                Album = Albums.Heart,
                PipelineId = HeartDepthImagePipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new HeartDepthImagePipelineConfig
                {
                    TreeSize = 2,
                    ImageSize = 500,
                    MaxDepth = depth
                }
            };
        }
    }
}
