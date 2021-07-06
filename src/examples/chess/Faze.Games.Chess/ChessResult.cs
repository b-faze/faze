using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Games.Chess
{
    public class ChessResult
    {
        public PlayerIndex WinningPlayerIndex { get; private set; }
        public bool IsCheckMate { get; private set; }

        internal static ChessResult CheckMate(PlayerIndex otherPlayer)
        {
            return new ChessResult
            {
                WinningPlayerIndex = otherPlayer,
                IsCheckMate = true
            };
        }
    }
}
