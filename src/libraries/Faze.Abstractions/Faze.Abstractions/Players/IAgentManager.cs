﻿namespace Faze.Abstractions.Players
{
    public interface IAgentManager
    {
        /// <summary>
        /// Keeps track and spawns new players as required
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <returns>Player at given index</returns>
        IPlayer GetPlayer(PlayerIndex playerIndex);
    }
}