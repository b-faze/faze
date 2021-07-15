using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System.Collections.Generic;

namespace Faze.Utilities.Testing
{
    public class TestGameState<TMove, TResult> : IGameState<TMove, TResult>
    {
        public IEnumerable<TMove> AvailableMoves { get; set; }
        public TResult Result { get; set; }

        public PlayerIndex CurrentPlayerIndex { get; set; }

        public IEnumerable<TMove> GetAvailableMoves() => AvailableMoves;

        public TResult GetResult() => Result;

        public IGameState<TMove, TResult> Move(TMove move)
        {
            return this;
        }
    }
}
