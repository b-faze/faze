using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Players
{
    /// <summary>
    /// Represents an AI for an arbitrary game
    /// </summary>
    public interface IPlayer
    {
        IMoveDistribution<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state);
    }

    /// <summary>
    /// Represents an AI for a generic game of known result type <typeparamref name="TResult"/>.
    /// The agent be able to evaluate the different results of a game
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IPlayer<TResult>
    {
        IMoveDistribution<TMove> GetMoves<TMove>(IGameState<TMove, TResult> state);
    }
}