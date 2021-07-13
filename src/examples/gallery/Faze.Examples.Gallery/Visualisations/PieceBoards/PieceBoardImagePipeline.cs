using Faze.Abstractions.Adapters;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.GridGames;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.PieceBoards
{
    public class PieceBoardImagePipeline
    {
        private readonly IGalleryService galleryService;

        public PieceBoardImagePipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IPipeline<IGameState<GridMove, SingleScoreResult?>> Create(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, int boardSize, ITreePainter<IGameState<GridMove, SingleScoreResult?>> painter)
        {
            return ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(painter)
                .GameTree(new SquareTreeAdapter(boardSize))
                .Build();
        }

    }
}
