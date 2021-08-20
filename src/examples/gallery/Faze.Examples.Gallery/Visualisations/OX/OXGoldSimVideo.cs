using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldSimVideo: IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return Variation1();
        }

        private GalleryItemMetadata Variation1()
        {
            var depth = 3;
            return new GalleryItemMetadata<OXGoldVideoSimPipelineConfig>
            {
                FileId = $"OX Gold {depth}.mp4",
                Album = Albums.OX,
                PipelineId = OXGoldVideoSimPipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OXGoldVideoSimPipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = depth,
                    BorderProportion = 0.07f,
                    LeafSimulations = 100
                }
            };
        }
    }

}
