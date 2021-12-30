using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldZoomVideo : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return Variation1();
        }

        private GalleryItemMetadata Variation1()
        {
            var depth = 4;
            return new GalleryItemMetadata<OX3DGoldVideoZoomPipelineConfig>
            {
                FileId = $"OX3D Gold Zoom {depth}.mp4",
                Album = Albums.OX3D,
                PipelineId = OX3DGoldVideoZoomPipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OX3DGoldVideoZoomPipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = depth,
                    BorderProportion = 0f,
                    TotalFrames = 50,
                    ZoomPath = new[] { 6, 4, 8, 7, 1, 0, 2, 5, 3, 4, 4, 8, 0, 8 },
                    LeafSimulations = 100
                }
            };
        }
    }

}
