using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using System.Collections.Generic;

namespace Faze.Examples.GridGames.PieceBoardStates
{
    public class KingsBoardState : PiecesBoardState
    {
        public KingsBoardState(int dimension) : base(dimension)
        {
        }

        private KingsBoardState(int dimension, IEnumerable<GridMove> influence, IEnumerable<GridMove> availableMoves, int score, bool fail)
            : base(dimension, influence, availableMoves, score, fail)
        {
        }

        protected override IGameState<GridMove, SingleScoreResult?> Create(int dimension, IEnumerable<GridMove> influence, IEnumerable<GridMove> availableMoves, int score, bool fail)
        {
            return new KingsBoardState(dimension, influence, availableMoves, score, fail);
        }

        protected override IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension)
        {
            var x = posIndex % dimension;
            var y = posIndex / dimension;

            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    var (x2, y2) = (x + i, y + j);

                    if (IsValidPos(x2, y2, dimension))
                        yield return y2 * dimension + x2;
                }
            }
        }

        private static bool IsValidPos(int x, int y, int dimension)
        {
            return x >= 0 && x < dimension
                          && y >= 0 && y < dimension;
        }
    }
}