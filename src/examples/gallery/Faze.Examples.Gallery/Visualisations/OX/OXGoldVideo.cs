using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVideo: IImageGenerator2
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return Variation1();
        }

        private GalleryItemMetadata Variation1()
        {
            var depth = 3;
            return new GalleryItemMetadata<OXGoldVideoPipelineConfig>
            {
                FileId = $"OX Gold {depth}.mp4",
                Album = Albums.OX,
                PipelineId = OXGoldVideoPipeline.Id,
                Depth = depth,
                Variation = "1",
                Config = new OXGoldVideoPipelineConfig
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
