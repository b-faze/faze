using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.GridGames.PieceBoardStates
{
    public class QueensBoardState : PiecesBoardState
    {
        public QueensBoardState(int dimension) : base(dimension)
        {
        }

        private QueensBoardState(int dimension, int pieces, IEnumerable<GridMove> availableMoves)
            : base(dimension, pieces, availableMoves)
        {
        }

        protected override IGameState<GridMove, SingleScoreResult?> Create(int dimension, int pieces, IEnumerable<GridMove> availableMoves)
        {
            return new QueensBoardState(dimension, pieces, availableMoves);
        }

        protected override IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension)
        {
            var x = posIndex % dimension;
            var y = posIndex / dimension;

            var horizontal = Enumerable.Range(0, dimension).Select(i => new GridMove(i, y, dimension));
            var vertical = Enumerable.Range(0, dimension).Select(i => new GridMove(x, i, dimension));
            var diagonals = GetDiagonals((x, y), dimension).Select(p => new GridMove(p.x, p.y, dimension));

            return horizontal.Concat(vertical).Concat(diagonals);
        }

        public static IEnumerable<(int x, int y)> GetDiagonals((int x, int y) pos, int dimension)
        {
            (int dx, int dy) direction = (1, 1);

            var minPos = Math.Min(pos.x, pos.y);
            (int x, int y) start = (pos.x - minPos, pos.y - minPos);
            var limit = dimension - Math.Max(start.x, start.y);
            for (var i = 0; i < limit; i++)
            {
                yield return (start.x + direction.dx * i, start.y + direction.dy * i);
            }

            direction = (1, -1);
            start = pos.x + pos.y < dimension - 1
                ? (0, pos.y + pos.x)
                : (pos.x - (dimension - 1 - pos.y), dimension - 1);

            limit = Math.Min(start.y + 1, dimension - start.x);
            for (var i = 0; i < limit; i++)
            {
                yield return (start.x + direction.dx * i, start.y + direction.dy * i);
            }
        }
    }
}