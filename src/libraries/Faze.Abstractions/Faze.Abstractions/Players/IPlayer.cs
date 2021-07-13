using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Players
{
    public interface IPlayer
    {
        IMoveDistribution<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state);
    }
}