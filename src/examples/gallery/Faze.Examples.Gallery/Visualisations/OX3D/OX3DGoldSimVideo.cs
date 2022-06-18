using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldSimVideo: IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return Variation1();
        }

        private GalleryItemMetadata Variation1()
        {
            var depth = 5;
            return new GalleryItemMetadata<OX3DGoldVideoSimPipelineConfig>
            {
                FileId = $"OX3D Gold {depth}.mp4",
                Album = Albums.OX3D,
                PipelineId = OX3DGoldVideoSimPipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OX3DGoldVideoSimPipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = depth,
                    BorderProportion = 0f,
                    LeafSimulations = 100
                }
            };
        }
    }

}
