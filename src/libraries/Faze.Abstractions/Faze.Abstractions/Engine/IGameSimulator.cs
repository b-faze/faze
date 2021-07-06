using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;

namespace Faze.Abstractions.Engine
{
    public interface IGameSimulator
    {
        TResult Simulate<TMove, TResult>(IGameState<TMove, TResult, IAgent> state);
    }
}