using Faze.Examples.Gallery.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuVis : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            var depth = 2;
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
                    MaxDepth = depth
                }
            };
        }
    }
}
