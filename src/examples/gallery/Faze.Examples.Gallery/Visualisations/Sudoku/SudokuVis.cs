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
            var depth = 3;
            yield return new GalleryItemMetadata<SudokuVideoPipelineConfig>
            {
                FileId = $"V1 Sudoku.mp4",
                Album = Albums.Sudoku,
                PipelineId = SudokuVideoPipeline.Id,
                Depth = depth,
                Variation = "Var 1",
                Config = new SudokuVideoPipelineConfig
                {
                    ImageSize = 500,
                    MaxDepth = depth,
                    LeafSimulations = 100
                }
            };
        }
    }
}
