using Faze.Examples.Chess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Games.Chess
{
    public struct ChessMove
    {
        internal readonly IGameMove move;

        internal ChessMove(IGameMove move)
        {
            this.move = move;
        }

        public static ChessMove Create(ChessSquares from, ChessSquares to)
        {
            return new ChessMove(Move.Create((Squares)from, (Squares)to));
        }

        public ChessSquares FromSquare() => (ChessSquares)move.FromSquare().Value;
        public ChessSquares ToSquare() => (ChessSquares)move.ToSquare().Value;
    }
}
