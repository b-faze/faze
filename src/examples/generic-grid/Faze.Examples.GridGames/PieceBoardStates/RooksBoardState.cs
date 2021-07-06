using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.GridGames.PieceBoardStates
{
    public class RooksBoardState : PiecesBoardState
    {
        public RooksBoardState(int dimension) : base(dimension)
        {
        }

        private RooksBoardState(int dimension, List<GridMove> pieces, IEnumerable<GridMove> availableMoves)
            : base(dimension, pieces, availableMoves)
        {
        }

        protected override IGameState<GridMove, SingleScoreResult?> Create(int dimension, List<GridMove> pieces, IEnumerable<GridMove> availableMoves)
        {
            return new RooksBoardState(dimension, pieces, availableMoves);
        }

        protected override IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension)
        {
            var x = posIndex % dimension;
            var y = posIndex / dimension;

            var horizontal = Enumerable.Range(0, dimension).Select(i => new GridMove(i, y, dimension));
            var vertical = Enumerable.Range(0, dimension).Select(i => new GridMove(x, i, dimension));

            return horizontal.Concat(vertical);
        }
    }
}