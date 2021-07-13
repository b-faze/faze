﻿using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.GridGames;
using Faze.Examples.GridGames.Pieces;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.PieceBoards
{
    public class PieceBoardVis : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly PieceBoardImagePipeline pipelineProvider;

        public PieceBoardVis(IGalleryService galleryService, PieceBoardImagePipeline pipelineProvider) 
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
            var maxDepth = 6;
            progress.SetMaxTicks(maxDepth);
            progress.SetMessage(Albums.PieceBoard);

            Run(progress.Spawn(), "Pawn", i => new PiecesBoardStateConfig(i, new PawnPiece()));
            progress.Tick();

            Run(progress.Spawn(), "Knight", i => new PiecesBoardStateConfig(i, new KnightPiece()));
            progress.Tick();

            Run(progress.Spawn(), "Bishop", i => new PiecesBoardStateConfig(i, new BishopPiece()));
            progress.Tick();

            Run(progress.Spawn(), "Rook", i => new PiecesBoardStateConfig(i, new RookPiece()));
            progress.Tick();

            Run(progress.Spawn(), "Queen", i => new PiecesBoardStateConfig(i, new QueenPiece()));
            progress.Tick();

            Run(progress.Spawn(), "King", i => new PiecesBoardStateConfig(i, new KingPiece()));
            progress.Tick();

            return Task.CompletedTask;
        }

        public Task Run(IProgressBar progress, string gameName, Func<int, PiecesBoardStateConfig> gameFn)
        {
            var maxBoardSize = 5;
            progress.SetMaxTicks(maxBoardSize);
            progress.SetMessage(gameName);

            for (var i = 3; i < maxBoardSize; i++)
            {
                Run(progress, gameName, new PiecesBoardState(gameFn(i)), i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressBar progress, string gameName, IGameState<GridMove, SingleScoreResult?> game, int boardSize)
        {
            var id = $"{gameName} Depth Painter {boardSize}.png";


            var metaData = new GalleryItemMetadata
            {
                Id = id,
                FileName = id,
                Albums = new[] { Albums.PieceBoard },
                Description = "Desc...",
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(boardSize, 500)
            {
                //MaxDepth = 3,
                //BorderProportions = 0.1f
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, boardSize, new BoardPainter());
            pipeline.Run(game, progress);

            return Task.CompletedTask;
        }
    }
}
