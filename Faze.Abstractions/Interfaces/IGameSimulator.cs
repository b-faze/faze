using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;

namespace Faze.Abstractions
{
    public interface IGameSimulator
    {
        TResult Simulate<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state);
    }
}