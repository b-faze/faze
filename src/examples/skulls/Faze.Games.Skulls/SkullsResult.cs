using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Games.Skulls
{
    public class SkullsResult<TPlayer>
    {
        public SkullsResult(TPlayer winningPlayer)
        {
            WinningPlayer = winningPlayer;
        }

        public TPlayer WinningPlayer { get; }

        public bool IsWinningPlayer(TPlayer player)
        {
            return WinningPlayer.Equals(player);
        }
    }
}
