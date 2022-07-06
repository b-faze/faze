using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuDepthVis : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            var depth = 3;
            yield return new GalleryItemMetadata<SudokuImagePipelineConfig>
            {
                FileId = $"V1 Sudoku.png",
                Album = Albums.Sudoku,
                PipelineId = SudokuImagePipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new SudokuImagePipelineConfig
                {
                    ImageSize = 500,
                    MaxDepth = depth,
                    BorderProportion = 0.1f
                }
            };

            yield return new GalleryItemMetadata<SudokuImagePipelineConfig>
            {
                FileId = $"V2 Sudoku.png",
                Album = Albums.Sudoku,
                PipelineId = SudokuImagePipeline.Id,
                Depth = depth,
                Variation = "Var 2",
                Config = new SudokuImagePipelineConfig
                {
                    ImageSize = 500,
                    MaxDepth = depth
                }
            };
        }
    }
}
