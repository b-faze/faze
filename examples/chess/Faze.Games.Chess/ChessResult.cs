using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Games.Chess
{
    public class ChessResult<TPlayer>
    {
        public TPlayer WinningPlayer { get; private set; }
        public bool IsCheckMate { get; private set; }

        internal static ChessResult<TPlayer> CheckMate(TPlayer otherPlayer)
        {
            return new ChessResult<TPlayer>
            {
                WinningPlayer = otherPlayer,
                IsCheckMate = true
            };
        }
    }
}
