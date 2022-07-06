using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldZoomVideo : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            yield return Variation1();
            yield return Variation2();
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
                    BorderProportion = 0f,
                    TotalFrames = 100,
                    ZoomPath = new[] { 1, 7, 0, 2, 4, 8 },
                }
            };
        }

        private GalleryItemMetadata Variation2()
        {
            var depth = 8;
            return new GalleryItemMetadata<OXGoldVideoZoomPipelineConfig>
            {
                FileId = $"OX Gold Zoom {depth}.mp4",
                Album = Albums.OX,
                PipelineId = OXGoldVideoZoomPipeline.Id,
                Depth = depth,
                Variation = "Var 2",
                Config = new OXGoldVideoZoomPipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = null,
                    BorderProportion = 0.0f,
                    RelativeDepthFactor = null,
                    TotalFrames = 200,
                    ZoomPath = new[] { 6, 4, 8, 7, 1, 0, 2, 5, 3 },
                }
            };
        }
    }

}
