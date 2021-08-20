using Faze.Abstractions.Core;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Games.Sudoku;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using System;
using System.Text;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuImagePipelineConfig : ISliceAndDiceTreeRendererPipelineConfig
    {
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int MaxDepth { get; set; }
    }
    public class SudokuImagePipeline : BaseVisualisationPipeline<SudokuImagePipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public SudokuImagePipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "Sudoku";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = null,
            RelativeCodePath = "Visualisations/Sudoku/SudokuImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<SudokuImagePipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetadata)
                .Render(new SliceAndDiceTreeRenderer(config.GetRendererOptions()))
                .Paint<SudokuStateWrapper>(new DepthTreePainter())
                .LimitDepth(config.MaxDepth)
                .GameTree(new SudokuTreeAdapter())
                .Build(() => new SudokuStateWrapper(SudokuState.Initial()));
        }
    }
}
