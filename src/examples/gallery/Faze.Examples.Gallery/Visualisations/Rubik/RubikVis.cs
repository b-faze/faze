using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.Rubik
{
    public class RubikVis : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            for (var depth = 1; depth < 5; depth++)
            {
                yield return GetVariation1(depth);
                yield return GetVariation2(depth);
                yield return GetVariation3(depth);
            }

        }

        private GalleryItemMetadata GetVariation1(int depth)
        {
            return new GalleryItemMetadata<RubikImagePipelineConfig>
            {
                FileId = $"Rubik_basic_{depth}.png",
                Album = Albums.Rubik,
                PipelineId = RubikImagePipeline.Id,
                Depth = depth,
                Variation = "basic",
                Config = new RubikImagePipelineConfig
                {
                    ImageSize = 500,
                    MaxDepth = depth,
                }
            };
        }

        private GalleryItemMetadata GetVariation2(int depth)
        {
            return new GalleryItemMetadata<RubikImagePipelineConfig>
            {
                FileId = $"Rubik_expanded_low_{depth}.png",
                Album = Albums.Rubik,
                PipelineId = RubikImagePipeline.Id,
                Depth = depth,
                Variation = "expanded low values",
                Config = new RubikImagePipelineConfig
                {
                    ImageSize = 500,
                    MaxDepth = depth,
                    MappingType = RubikMappingType.ExpandLow,
                }
            };
        }

        private GalleryItemMetadata GetVariation3(int depth)
        {
            return new GalleryItemMetadata<RubikImagePipelineConfig>
            {
                FileId = $"Rubik_unavailable_{depth}.png",
                Album = Albums.Rubik,
                PipelineId = RubikImagePipeline.Id,
                Depth = depth,
                Variation = "unavailable",
                Config = new RubikImagePipelineConfig
                {
                    ImageSize = 500,
                    MaxDepth = depth,
                    UnavailablePaintType = RubikUnavailablePaintType.Black
                }
            };
        }
    }
}
