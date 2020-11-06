using Rudz.Chess.Enums;
using Rudz.Chess.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Games.Chess
{
    public struct ChessMove
    {
        internal readonly Move move;

        internal ChessMove(Move move)
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
