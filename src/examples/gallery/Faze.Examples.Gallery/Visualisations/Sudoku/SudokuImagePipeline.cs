using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
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
using System.Collections.Generic;
using System.Text;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int MaxDepth { get; set; }
    }
    public class SudokuImagePipeline : BaseVisualisationPipeline<SudokuImagePipelineConfig>
    {
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public SudokuImagePipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
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
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint<IGameState<SudokuMove, WinLoseDrawResult?>>(new DepthTreePainter())
                .GameTree(new SudokuTreeAdapter())
                .Build(() => SudokuState.Initial());
        }
    }

    public class SudokuTreeAdapter : IGameStateTreeAdapter<SudokuMove>
    {
        public IEnumerable<IGameState<SudokuMove, TResult>> GetChildren<TResult>(IGameState<SudokuMove, TResult> state)
        {
            var availableMoves = state.GetAvailableMoves();
            return null;
        }
    }
}
