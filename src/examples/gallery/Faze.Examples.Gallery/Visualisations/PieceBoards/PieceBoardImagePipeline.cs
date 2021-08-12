using Faze.Core.Adapters;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Games.GridGames;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faze.Core.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;

namespace Faze.Examples.Gallery.Visualisations.PieceBoards
{
    public class PieceBoardImagePipeline
    {
        private readonly IGalleryService galleryService;

        public PieceBoardImagePipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IPipeline<IGameState<GridMove, SingleScoreResult?>> Create(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, int boardSize)
        {
            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(new PiecesBoardPainter())
                .GameTree(new SquareTreeAdapter(boardSize))
                .Build();
        }

    }
}
