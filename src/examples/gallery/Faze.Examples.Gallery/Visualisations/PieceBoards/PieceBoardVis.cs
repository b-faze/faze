using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.PieceBoards
{
    public class PieceBoardVis : IGalleryItemProvider
    {
        public IEnumerable<GalleryItemMetadata> GetMetaData()
        {
            var maxDepth = 6;

            for (var boardSize = 3; boardSize < 5; boardSize++)
            {
                yield return GetVariation(boardSize, maxDepth, PieceConfigType.Pawn);
                yield return GetVariation(boardSize, maxDepth, PieceConfigType.Knight);
                yield return GetVariation(boardSize, maxDepth, PieceConfigType.Bishop);
                yield return GetVariation(boardSize, maxDepth, PieceConfigType.Rook);
                yield return GetVariation(boardSize, maxDepth, PieceConfigType.Queen);
                yield return GetVariation(boardSize, maxDepth, PieceConfigType.King);
            }
        }

        private GalleryItemMetadata GetVariation(int boardSize, int maxDepth, PieceConfigType piece)
        {
            return new GalleryItemMetadata<PieceBoardImagePipelineConfig>
            {
                FileId = $"{piece} Depth Painter {boardSize}.png",
                Album = Albums.PieceBoard,
                PipelineId = PieceBoardImagePipeline.Id,
                Depth = maxDepth,
                Variation = piece.ToString(),
                Config = new PieceBoardImagePipelineConfig
                {
                    TreeSize = boardSize,
                    ImageSize = 500,
                    MaxDepth = maxDepth,
                    Piece = piece,
                    OnlySafeMoves = false
                }
            };
        }
    }
}
