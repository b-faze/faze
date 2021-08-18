using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldVis : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            for (var depth = 1; depth < 6; depth++)
            {
                yield return GetVariation1(depth);
                yield return GetVariation2(depth);
            }
        }

        private GalleryItemMetadata GetVariation1(int depth)
        {
            return new GalleryItemMetadata<OX3DGoldImagePipelineConfig>
            {
                FileId = $"OX3D Gold {depth} sim.png",
                Album = Albums.OX3D,
                PipelineId = OX3DGoldImagePipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new OX3DGoldImagePipelineConfig
                {
                    TreeSize = 3,
                    ImageSize = 500,
                    MaxDepth = depth
                }
            };
        }        
        
        private GalleryItemMetadata GetVariation2(int depth)
        {
            return new GalleryItemMetadata<OX3DGoldImagePipelineConfig>
            {
                FileId = $"V2 OX3D Gold {depth} sim.png",
                Album = Albums.OX3D,
                PipelineId = OX3DGoldImagePipeline.Id,
                Depth = depth,
                Variation = "Var 2",
                Config = new OX3DGoldImagePipelineConfig
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
