using Faze.Abstractions.Players;
using System.Collections.Generic;

namespace Faze.Abstractions.GameStates
{
    /// <summary>
    /// Provides core functionality of a game state,
    /// which is needed for automating game play and building game trees
    /// </summary>
    /// <typeparam name="TMove">Type of game move</typeparam>
    /// <typeparam name="TResult">Type of game result</typeparam>
    public interface IGameState<TMove, out TResult> : IGameResult<TResult>
    {
        PlayerIndex CurrentPlayerIndex { get; }

        IEnumerable<TMove> GetAvailableMoves();

        IGameState<TMove, TResult> Move(TMove move);
    }
}