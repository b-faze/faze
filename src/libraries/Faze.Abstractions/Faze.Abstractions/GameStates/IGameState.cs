using Faze.Abstractions.Players;
using System.Collections.Generic;

namespace Faze.Abstractions.GameStates
{
    public interface IGameState<TMove, out TResult> : IGameResult<TResult>
    {
        PlayerIndex CurrentPlayerIndex { get; }

        IEnumerable<TMove> GetAvailableMoves();

        IGameState<TMove, TResult> Move(TMove move);
    }
}