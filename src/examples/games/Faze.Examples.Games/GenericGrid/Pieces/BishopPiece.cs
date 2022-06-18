using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.GridGames.Pieces
{
    public class BishopPiece : IPiece
    {
        public IEnumerable<GridMove> GetPieceMoves(GridMove pos, int dimension)
        {
            return pos.GetDiagonals(dimension);
        }
    }
}