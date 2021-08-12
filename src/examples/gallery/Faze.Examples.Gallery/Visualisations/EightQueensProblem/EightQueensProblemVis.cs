using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemVis : IImageGenerator2
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            for (var depth = 1; depth < 4; depth++)
            {
                yield return Variation1(depth);
                yield return Variation2(depth);
                yield return Variation3(depth);
                yield return Variation4(depth);
            }

        }

        private GalleryItemMetadata Variation1(int depth)
        {
            return new GalleryItemMetadata<EightQueensProblemImagePipelineConfig>
            {
                FileName = $"8 Queens Problem Solutions depth {depth}.png",
                Album = Albums.EightQueensProblem,
                PipelineId = EightQueensProblemImagePipeline.Id,
                Variation = 1,
                Depth = depth,
                Config = new EightQueensProblemImagePipelineConfig
                {
                    TreeSize = 8,
                    ImageSize = 600,
                    MaxDepth = depth,
                    BlackUnavailableMoves = true,
                    BlackParentMoves = true
                }
            };
        }

        private GalleryItemMetadata Variation2(int depth)
        {
            return new GalleryItemMetadata<EightQueensProblemImagePipelineConfig>
            {
                FileName = $"Var 2 8QP Solutions depth {depth}.png",
                Album = Albums.EightQueensProblem,
                PipelineId = EightQueensProblemImagePipeline.Id,
                Variation = 1,
                Depth = depth,
                Config = new EightQueensProblemImagePipelineConfig
                {
                    TreeSize = 8,
                    ImageSize = 600,
                    MaxDepth = depth,
                    BlackUnavailableMoves = true,
                }
            };
        }        
        
        private GalleryItemMetadata Variation3(int depth)
        {
            return new GalleryItemMetadata<EightQueensProblemImagePipelineConfig>
            {
                FileName = $"Var 3 8QP Solutions depth {depth}.png",
                Album = Albums.EightQueensProblem,
                PipelineId = EightQueensProblemImagePipeline.Id,
                Variation = 1,
                Depth = depth,
                Config = new EightQueensProblemImagePipelineConfig
                {
                    TreeSize = 8,
                    ImageSize = 600,
                    MaxDepth = depth,
                }
            };
        }

        private GalleryItemMetadata Variation4(int depth)
        {
            return new GalleryItemMetadata<EightQueensProblemImagePipelineConfig>
            {
                FileName = $"Var 4 8QP Solutions depth {depth}.png",
                Album = Albums.EightQueensProblem,
                PipelineId = EightQueensProblemImagePipeline.Id,
                Variation = 1,
                Depth = depth,
                Config = new EightQueensProblemImagePipelineConfig
                {
                    TreeSize = 8,
                    ImageSize = 600,
                    MaxDepth = depth,
                    BorderProportion = 0.1f
                }
            };
        }
    }
}
