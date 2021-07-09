using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.GridGames.PieceBoardStates
{
    public class PawnsBoardState : PiecesBoardState
    {
        public PawnsBoardState(int dimension) : base(dimension)
        {
        }

        private PawnsBoardState(int dimension, IEnumerable<GridMove> influence, IEnumerable<GridMove> availableMoves, int score, bool fail)
            : base(dimension, influence, availableMoves, score, fail)
        {
        }

        protected override IGameState<GridMove, SingleScoreResult?> Create(int dimension, IEnumerable<GridMove> influence, IEnumerable<GridMove> availableMoves, int score, bool fail)
        {
            return new PawnsBoardState(dimension, influence, availableMoves, score, fail);
        }

        protected override IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension)
        {
            var x = posIndex % dimension;
            var y = posIndex / dimension;

            var directions = new (int x, int y)[] { (-1, -1), (1, -1), (-1, 1), (1, 1) };

            return directions
                .Select(d => new { x = d.x + x, y = d.y + y })
                .Where(p => IsValidPos(p.x, p.y, dimension))
                .Select(p => new GridMove(p.x, p.y, dimension));
        }

        private static bool IsValidPos(int x, int y, int dimension)
        {
            return x >= 0 && x < dimension
                          && y >= 0 && y < dimension;
        }
    }
}