using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.GridGames.Pieces
{
    public class RookPiece : IPiece
    {
        public IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension)
        {
            var x = posIndex % dimension;
            var y = posIndex / dimension;

            var horizontal = Enumerable.Range(0, dimension).Select(i => new GridMove(i, y, dimension));
            var vertical = Enumerable.Range(0, dimension).Select(i => new GridMove(x, i, dimension));

            return horizontal.Concat(vertical);
        }
    }
}