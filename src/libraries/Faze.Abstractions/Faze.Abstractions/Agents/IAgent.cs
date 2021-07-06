using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Players
{
    public interface IAgent
    {
        TMove ChooseMove<TMove, TResult, TPlayer>(IGameState<TMove, TResult, TPlayer> state);
    }
}