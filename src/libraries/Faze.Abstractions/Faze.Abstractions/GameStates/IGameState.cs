using System.Collections.Generic;

namespace Faze.Abstractions.GameStates
{
    public interface IGameState<TMove, out TResult, out TPlayer>
    {
        TPlayer GetCurrentPlayer();

        IEnumerable<TMove> GetAvailableMoves();

        TResult GetResult();

        IGameState<TMove, TResult, TPlayer> Move(TMove move);
    }
}