using Faze.Abstractions.GameResults;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Games.Skulls
{
    public class SkullsResult
    {
        public SkullsResult(PlayerIndex winningPlayer)
        {
            WinningPlayerIndex = winningPlayer;
        }

        public PlayerIndex WinningPlayerIndex { get; }

        public bool IsWinningPlayer(PlayerIndex player)
        {
            return WinningPlayerIndex.Equals(player);
        }
    }
}
