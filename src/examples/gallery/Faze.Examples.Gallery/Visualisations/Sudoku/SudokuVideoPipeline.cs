using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Gallery.Visualisations.OX;
using Faze.Examples.Games.Sudoku;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using Faze.Core.TreeLinq;
using System;
using System.Text;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.GameMoves;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuVideoPipelineConfig : ISliceAndDiceTreeRendererPipelineConfig
    {
        public int ImageSize { get; set; }

        public float BorderProportion { get; set; }
        public int MaxDepth { get; set; }

        public int LeafSimulations { get; set; }
}
    public class SudokuVideoPipeline : BaseVisualisationPipeline<SudokuVideoPipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public SudokuVideoPipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "Sudoku Video";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = null,
            RelativeCodePath = "Visualisations/Sudoku/SudokuVideoPipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<SudokuVideoPipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;

            return ReversePipelineBuilder.Create()
                .GalleryVideo(galleryService, galleryMetadata)
                .Merge()
                .Map(builder => builder
                    .Render(new SliceAndDiceTreeRenderer(config.GetRendererOptions()))
                    .Paint(new GoldInterpolator())
                    .Map<double, double>(t => t.NormaliseSiblings())
                    .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                )
                .Iterate(new WinLoseDrawResultsTreeIterator(new GameSimulator(), config.LeafSimulations))
                .LimitDepth(config.MaxDepth)
                .Map<IGameState<GridMove, WinLoseDrawResult?>, SudokuStateWrapper>(t => t.MapValue(v => (IGameState<GridMove, WinLoseDrawResult?>)v))
                .GameTree(new SudokuTreeAdapter())
                .Build(() => new SudokuStateWrapper(SudokuState.Initial()));
        }
    }
}
