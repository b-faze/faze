using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Players
{
    public interface IPlayer
    {
        PossibleMoves<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state);
    }
}