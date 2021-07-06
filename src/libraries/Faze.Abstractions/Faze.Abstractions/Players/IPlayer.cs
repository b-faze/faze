using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Players
{
    public interface IPlayer
    {
        TMove ChooseMove<TMove, TResult, TPlayer>(IGameState<TMove, TResult, TPlayer> state);
    }
}