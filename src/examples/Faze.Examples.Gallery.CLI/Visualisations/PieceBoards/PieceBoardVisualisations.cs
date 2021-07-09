using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.GridGames.PieceBoardStates;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.PieceBoards
{
    public class PieceBoardVisualisations : IImageGenerator
    {
        private readonly PieceBoardImagePipeline pipelineProvider;

        public PieceBoardVisualisations(PieceBoardImagePipeline pipelineProvider) 
        {
            this.pipelineProvider = pipelineProvider;
        }

        public Task Generate(IProgressBar progress)
        {
            var maxDepth = 6;
            progress.SetMaxTicks(maxDepth);
            progress.SetMessage("Piece Board");

            Run(progress.Spawn(), "Pawn", i => new PawnsBoardState(i));
            progress.Tick();

            Run(progress.Spawn(), "Knight", i => new KnightsBoardState(i));
            progress.Tick();

            Run(progress.Spawn(), "Bishop", i => new BishopsBoardState(i));
            progress.Tick();

            Run(progress.Spawn(), "Rook", i => new RooksBoardState(i));
            progress.Tick();

            Run(progress.Spawn(), "Queen", i => new QueensBoardState(i));
            progress.Tick();

            Run(progress.Spawn(), "King", i => new KingsBoardState(i));
            progress.Tick();

            return Task.CompletedTask;
        }

        public Task Run(IProgressBar progress, string gameName, Func<int, IGameState<GridMove, SingleScoreResult?>> gameFn)
        {
            var maxBoardSize = 5;
            progress.SetMaxTicks(maxBoardSize);
            progress.SetMessage(gameName);

            for (var i = 3; i < maxBoardSize; i++)
            {
                Run(progress, gameFn(i), i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressBar progress, IGameState<GridMove, SingleScoreResult?> game, int boardSize)
        {
            var pieceBoardType = game.GetType().Name;
            var id = $"{pieceBoardType}DepthPainter{boardSize}";

            var metaData = new GalleryItemMetadata
            {
                Id = id,
                FileName = $"{pieceBoardType} Depth Painter {boardSize}.png",
                Albums = new[] { "Piece Board" },
                Description = "Desc...",
            };

            var rendererConfig = new SquareTreeRendererOptions(boardSize, 500)
            {
                //MaxDepth = 3,
                //BorderProportions = 0.1f
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, boardSize);
            pipeline.Run(game, progress);

            return Task.CompletedTask;
        }
    }
}
