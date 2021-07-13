using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.GridGames;
using Faze.Examples.GridGames.Pieces;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faze.Examples.Gallery.Services.Aggregates;
using Faze.Examples.Gallery.Visualisations.EightQueensProblem.DataGenerators;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemVis : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly ITreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;

        public EightQueensProblemVis(IGalleryService galleryService, ITreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider) 
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public ImageGeneratorMetaData GetMetaData()
        {
            return new ImageGeneratorMetaData(new[] { Albums.EightQueensProblem });
        }

        public Task Generate(IProgressBar progress)
        {
            var maxDepth = 3;
            progress.SetMaxTicks(maxDepth);
            progress.SetMessage(Albums.EightQueensProblem);

            for (var i = 1; i <= maxDepth; i++)
            {
                Run(progress, i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressBar progress, int maxDepth)
        {
            var id = $"8 Queens Problem Solutions depth {maxDepth}.png";


            var metaData = new GalleryItemMetadata
            {
                Id = id,
                FileName = id,
                Albums = new[] { Albums.EightQueensProblem },
                Description = "Desc...",
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(8, 600)
            {
                MaxDepth = maxDepth,
                //BorderProportions = 0.1f
            };

            var pipeline = Create(metaData, rendererConfig, new EightQueensProblemPainter());
            pipeline.Run();

            return Task.CompletedTask;
        }

        public IPipeline Create(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, ITreePainter<EightQueensProblemSolutionAggregate> painter)
        {
            return ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(painter)
                .LoadTree(EightQueensProblemExhaustiveDataPipeline.Id, treeDataProvider);
        }
    }
}
