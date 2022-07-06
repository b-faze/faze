using Faze.Core.Adapters;
using Faze.Abstractions.Core;
using Faze.Core.Pipelines;
using Faze.Examples.Games.GridGames;
using Faze.Rendering.TreeRenderers;
using System;
using Faze.Core.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Games.GridGames.Pieces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Faze.Examples.Gallery.Visualisations.PieceBoards
{
    public class PieceBoardImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PieceConfigType Piece { get; set; }
        public bool OnlySafeMoves { get; set; }
        public float? RelativeDepthFactor { get; set; }
    }

    public class PieceBoardImagePipeline : BaseVisualisationPipeline<PieceBoardImagePipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public PieceBoardImagePipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "PB Depth";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = null,
            RelativeCodePath = "Visualisations/PieceBoards/PieceBoardImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<PieceBoardImagePipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;
            var state = new PiecesBoardState(new PiecesBoardStateConfig(config.TreeSize, GetPiece(config.Piece), config.OnlySafeMoves));

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetadata)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new PiecesBoardPainter())
                .GameTree(new SquareTreeAdapter(config.TreeSize))
                .Build(() => state);
        }

        private IPiece GetPiece(PieceConfigType piece)
        {
            switch (piece)
            {
                case PieceConfigType.Pawn:
                    return new PawnPiece();

                case PieceConfigType.Knight:
                    return new KnightPiece();

                case PieceConfigType.Bishop:
                    return new BishopPiece();

                case PieceConfigType.Rook:
                    return new RookPiece();

                case PieceConfigType.Queen:
                    return new QueenPiece();

                case PieceConfigType.King:
                    return new KingPiece();
            }

            throw new NotSupportedException($"Unknown piece type '{piece}'");
        }

    }
}
