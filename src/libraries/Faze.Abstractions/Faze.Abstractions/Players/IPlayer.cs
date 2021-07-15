using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Players
{
    public interface IPlayer
    {
        IMoveDistribution<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state);
    }

    public interface IPlayer<TResult>
    {
        IMoveDistribution<TMove> GetMoves<TMove>(IGameState<TMove, TResult> state);
    }
}