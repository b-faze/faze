using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Faze.Examples.Gallery.Visualisations.Heart
{
    public class HeartGold : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            for (var depth = 1; depth < 8; depth++)
            {
                yield return GetVariation1(depth);
            }
        }

        private GalleryItemMetadata GetVariation1(int depth)
        {
            return new GalleryItemMetadata<HeartGoldImagePipelineConfig>
            {
                FileId = $"V1 Heart Gold {depth}.png",
                Album = Albums.Heart,
                PipelineId = HeartGoldImagePipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new HeartGoldImagePipelineConfig
                {
                    TreeSize = 2,
                    ImageSize = 500,
                    MaxDepth = depth
                }
            };
        }
    }
}
