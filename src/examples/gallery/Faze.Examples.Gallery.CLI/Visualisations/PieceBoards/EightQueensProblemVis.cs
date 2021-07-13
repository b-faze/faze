using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.GridGames.PieceBoardStates;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.PieceBoards
{
    public class EightQueensProblemVis : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly PieceBoardImagePipeline pipelineProvider;

        public EightQueensProblemVis(IGalleryService galleryService, PieceBoardImagePipeline pipelineProvider) 
        {
            this.galleryService = galleryService;
            this.pipelineProvider = pipelineProvider;
        }

        public ImageGeneratorMetaData GetMetaData()
        {
            return new ImageGeneratorMetaData
            {
                Albums = new[] { Albums.PieceBoard }
            };
        }

        public Task Generate(IProgressBar progress)
        {
            var maxDepth = 3;
            progress.SetMaxTicks(maxDepth);
            progress.SetMessage(Albums.PieceBoard);

            for (var i = 1; i < maxDepth; i++)
            {
                Run(progress, new QueensBoardState(8), i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressBar progress, IGameState<GridMove, SingleScoreResult?> game, int maxDepth)
        {
            var id = $"8 Queens Problem Solutions depth {maxDepth}.png";


            var metaData = new GalleryItemMetadata
            {
                Id = id,
                FileName = id,
                Albums = new[] { Albums.PieceBoard },
                Description = "Desc...",
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(8, 500)
            {
                MaxDepth = maxDepth,
                //BorderProportions = 0.1f
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, maxDepth, new BoardPainter());
            pipeline.Run(game, progress);

            return Task.CompletedTask;
        }
    }
}
