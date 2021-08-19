using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Core.Extensions;
using System.Collections.Generic;

namespace Faze.Examples.Games.GridGames.Pieces
{
    public class KnightPiece : IPiece
    {
        private static readonly List<(int x, int y)> KnightsOffsets = new List<(int x, int y)>
        {
            (1, -2), (2, -1), (2, 1), (1, 2), (-1, 2), (-2, 1), (-2, -1), (-1, -2)
        };

        public IEnumerable<GridMove> GetPieceMoves(GridMove pos, int dimension)
        {
            var x = pos.GetX(dimension);
            var y = pos.GetY(dimension);

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