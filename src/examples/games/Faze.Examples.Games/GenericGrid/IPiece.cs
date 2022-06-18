using Faze.Abstractions.GameMoves;
using System.Collections.Generic;

namespace Faze.Examples.Games.GridGames
{
    public interface IPiece
    {
        IEnumerable<GridMove> GetPieceMoves(GridMove pos, int dimension);
    }

}
