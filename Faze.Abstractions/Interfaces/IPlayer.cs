using Faze.Abstractions;

namespace Faze.Abstractions
{
    public interface IPlayer
    {
        TMove ChooseMove<TMove, TResult, TPlayer>(IGameState<TMove, TResult, TPlayer> state);
    }
}