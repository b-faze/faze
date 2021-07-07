using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System.Collections.Generic;

namespace Faze.Abstractions.Engine
{
    public interface IGameSimulator
    {
        TResult Simulate<TMove, TResult>(IGameState<TMove, TResult> state);
        IEnumerable<TResult> SampleResults<TMove, TResult>(IGameState<TMove, TResult> state, int n);
    }
}