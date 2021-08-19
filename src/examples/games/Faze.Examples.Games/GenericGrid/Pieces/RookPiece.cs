using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.GridGames.Pieces
{
    public class RookPiece : IPiece
    {
        public IEnumerable<GridMove> GetPieceMoves(GridMove pos, int dimension)
        {
            var horizontal = pos.GetRow(dimension);
            var vertical = pos.GetColumn(dimension);

            return horizontal.Concat(vertical);
        }
    }
}