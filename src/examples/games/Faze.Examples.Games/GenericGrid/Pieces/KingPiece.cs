using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Core.Extensions;
using System.Collections.Generic;

namespace Faze.Examples.Games.GridGames.Pieces
{
    public class KingPiece : IPiece
    {
        public IEnumerable<GridMove> GetPieceMoves(GridMove pos, int dimension)
        {
            var x = pos.GetX(dimension);
            var y = pos.GetY(dimension);

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