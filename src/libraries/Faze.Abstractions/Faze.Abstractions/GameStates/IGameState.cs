using Faze.Abstractions.Players;
using System.Collections.Generic;

namespace Faze.Abstractions.GameStates
{
    public interface IGameState<TMove, out TResult>
    {
        PlayerIndex CurrentPlayerIndex { get; }

        IEnumerable<TMove> GetAvailableMoves();

        TResult GetResult();

        IGameState<TMove, TResult> Move(TMove move);
    }
}