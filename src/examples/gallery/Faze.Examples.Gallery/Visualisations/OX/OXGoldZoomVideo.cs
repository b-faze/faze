using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldZoomVideo : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return Variation1();
        }

        private GalleryItemMetadata Variation1()
        {
            var depth = 3;
            return new GalleryItemMetadata<OXGoldVideoZoomPipelineConfig>
            {
                FileId = $"OX Gold Zoom {depth}.mp4",
                Album = Albums.OX,
                PipelineId = OXGoldVideoZoomPipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OXGoldVideoZoomPipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = null,
                    BorderProportion = 0.07f,
                    ZoomPath = new[] { 1, 7, 0, 2, 4, 8, 6, 5 }
                }
            };
        }
    }

}
