using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using System.Collections.Generic;

namespace Faze.Examples.GridGames.PieceBoardStates
{
    public class KnightsBoardState : PiecesBoardState
    {
        private static readonly List<(int x, int y)> KnightsOffsets = new List<(int x, int y)>
        {
            (1, -2), (2, -1), (2, 1), (1, 2), (-1, 2), (-2, 1), (-2, -1), (-1, -2)
        };

        public KnightsBoardState(int dimension) : base(dimension)
        {
        }

        private KnightsBoardState(int dimension, int pieces, IEnumerable<GridMove> availableMoves)
            : base(dimension, pieces, availableMoves)
        {
        }

        protected override IGameState<GridMove, SingleScoreResult?> Create(int dimension, int pieces, IEnumerable<GridMove> availableMoves)
        {
            return new KnightsBoardState(dimension, pieces, availableMoves);
        }

        protected override IEnumerable<GridMove> GetPieceMoves(int knightPosIndex, int dimension)
        {
            var x = knightPosIndex % dimension;
            var y = knightPosIndex / dimension;

            foreach (var offset in KnightsOffsets)
            {
                var x2 = x + offset.x;
                var y2 = y + offset.y;

                if (IsValidPos(x2, y2, dimension))
                    yield return y2 * dimension + x2;
            }
        }

        private static bool IsValidPos(int x, int y, int dimension)
        {
            return x >= 0 && x < dimension
                          && y >= 0 && y < dimension;
        }
    }
}