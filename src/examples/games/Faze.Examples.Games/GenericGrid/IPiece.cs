using Faze.Abstractions.GameMoves;
using System.Collections.Generic;

namespace Faze.Examples.GridGames
{
    public interface IPiece
    {
        IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension);
    }

}
