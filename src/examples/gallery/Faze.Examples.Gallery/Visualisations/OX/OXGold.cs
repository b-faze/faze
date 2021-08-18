using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGold : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            for (var depth = 1; depth < 8; depth++)
            {
                yield return GetVariation1(depth);
            }

            for (var depth = 1; depth < 5; depth++)
            {
                yield return GetVariation2(depth);
            }

        }

        private GalleryItemMetadata GetVariation1(int depth)
        {
            return new GalleryItemMetadata<OXGoldImagePipelineConfig>
            {
                FileId = $"V1 OX Gold {depth}.png",
                Album = Albums.OX,
                PipelineId = OXGoldImagePipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OXGoldImagePipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = depth
                }
            };
        }

        private GalleryItemMetadata GetVariation2(int depth) 
        {
            return new GalleryItemMetadata<OXGoldImagePipelineConfig>
            {
                FileId = $"V2 OX Gold {depth}.png",
                Album = Albums.OX,
                PipelineId = OXGoldImagePipeline.Id,
                Depth = depth,
                Variation = "Var 2",
                Config = new OXGoldImagePipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = depth,
                    BorderProportion = 0.07f
                }
            };
        }
    }

}
