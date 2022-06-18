using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.GridGames.Pieces
{
    public class QueenPiece : IPiece
    {
        public IEnumerable<GridMove> GetPieceMoves(GridMove pos, int dimension)
        {
            var horizontal = pos.GetRow(dimension); 
            var vertical = pos.GetColumn(dimension);
            var diagonals = pos.GetDiagonals(dimension);

            return horizontal.Concat(vertical).Concat(diagonals);
        }


    }
}