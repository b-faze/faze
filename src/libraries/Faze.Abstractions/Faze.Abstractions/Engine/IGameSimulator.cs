using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System.Collections.Generic;

namespace Faze.Abstractions.Engine
{
    /// <summary>
    /// Provides the ability to play a game and gather results
    /// </summary>
    public interface IGameSimulator
    {
        TResult Simulate<TMove, TResult>(IGameState<TMove, TResult> state);
    }
}