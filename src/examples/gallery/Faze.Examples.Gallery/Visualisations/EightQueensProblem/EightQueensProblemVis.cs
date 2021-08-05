using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Games.GridGames;
using Faze.Examples.Games.GridGames.Pieces;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faze.Examples.Gallery.Services.Aggregates;
using Faze.Examples.Gallery.Visualisations.EightQueensProblem.DataGenerators;
using Faze.Core.Extensions;
using Faze.Core.TreeMappers;
using Faze.Core.IO;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Gallery.Services.Serialisers;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemVis : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;

        public EightQueensProblemVis(IGalleryService galleryService, IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider) 
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
            //var treeSerialiser = new LeafTreeSerialiser<EightQueensProblemSolutionAggregate>(new EightQueensProblemSolutionAggregateSerialiser(), new LeafTreeSerialiserConfig<EightQueensProblemSolutionAggregate>
            //{
            //    AddTree = new AddTree<EightQueensProblemSolutionAggregate>((existing, value) =>
            //    {
            //        if (existing == null)
            //            existing = new EightQueensProblemSolutionAggregate();

            //        existing.Add(value);
            //        return existing;
            //    }, addWhileTraversing: true)
            //});
            //this.treeDataProvider = new GalleryTreeDataProvider<EightQueensProblemSolutionAggregate>(galleryService, treeSerialiser);
        }

        public ImageGeneratorMetaData GetMetaData()
        {
            return new ImageGeneratorMetaData(new[] { Albums.EightQueensProblem });
        }

        public Task Generate(IProgressTracker progress)
        {
            var maxDepth = 3;
            progress.SetMaxTicks(maxDepth * 4);
            progress.SetMessage(Albums.EightQueensProblem);

            for (var i = 1; i <= maxDepth; i++)
            {
                RunVariation1(progress, i);
                progress.Tick();

                RunVariation2(progress, i);
                progress.Tick();

                RunVariation3(progress, i);
                progress.Tick();

                RunVariation4(progress, i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task RunVariation1(IProgressTracker progress, int maxDepth)
        {
            var id = $"8 Queens Problem Solutions depth {maxDepth}.png";


            var metaData = new GalleryItemMetadata
            {
                FileName = id,
                Album = Albums.EightQueensProblem,
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(8, 600)
            {
                MaxDepth = maxDepth,
                //BorderProportions = 0.1f
            };

            var painterConfig = new EightQueensProblemPainterConfig
            {
                BlackParentMoves = false,
                BlackUnavailableMoves = true
            };

            var pipeline = Create(metaData, rendererConfig, painterConfig);
            pipeline.Run();

            return Task.CompletedTask;
        }

        private Task RunVariation2(IProgressTracker progress, int maxDepth)
        {
            var id = $"Var 2 8QP Solutions depth {maxDepth}.png";


            var metaData = new GalleryItemMetadata
            {
                FileName = id,
                Album = Albums.EightQueensProblem,
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(8, 600)
            {
                MaxDepth = maxDepth,
                //BorderProportions = 0.1f
            };

            var painterConfig = new EightQueensProblemPainterConfig
            {
                BlackParentMoves = true,
                BlackUnavailableMoves = true
            };

            var pipeline = Create(metaData, rendererConfig, painterConfig);
            pipeline.Run();

            return Task.CompletedTask;
        }

        private Task RunVariation3(IProgressTracker progress, int maxDepth)
        {
            var id = $"Var 3 8QP Solutions depth {maxDepth}.png";


            var metaData = new GalleryItemMetadata
            {
                FileName = id,
                Album = Albums.EightQueensProblem,
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(8, 600)
            {
                MaxDepth = maxDepth,
                //BorderProportions = 0.1f
            };

            var painterConfig = new EightQueensProblemPainterConfig
            {
                BlackParentMoves = false,
                BlackUnavailableMoves = false
            };

            var pipeline = Create(metaData, rendererConfig, painterConfig);
            pipeline.Run();

            return Task.CompletedTask;
        }

        private Task RunVariation4(IProgressTracker progress, int maxDepth)
        {
            var id = $"Var 4 8QP Solutions depth {maxDepth}.png";


            var metaData = new GalleryItemMetadata
            {
                FileName = id,
                Album = Albums.EightQueensProblem,
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(8, 600)
            {
                MaxDepth = maxDepth,
                BorderProportion = 0.1f
            };

            var painterConfig = new EightQueensProblemPainterConfig
            {
                BlackParentMoves = false,
                BlackUnavailableMoves = false
            };

            var pipeline = Create(metaData, rendererConfig, painterConfig);
            pipeline.Run();

            return Task.CompletedTask;
        }

        public IPipeline Create(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, EightQueensProblemPainterConfig painterConfig)
        {
            return ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(new EightQueensProblemPainter(painterConfig))
                .Map(new MoveOrderInvariantTreeMapper())
                .LoadTree(EightQueensProblemExhaustiveDataPipeline.Id, treeDataProvider);
        }
    }
}
