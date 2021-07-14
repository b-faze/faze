using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Core.Tests.Utilities
{
    class TestGameState : IGameState<GridMove, TestGameResult>
    {
        public IEnumerable<GridMove> AvailableMoves { get; set; }
        public TestGameResult Result { get; set; }

        public PlayerIndex CurrentPlayerIndex { get; set; }

        public IEnumerable<GridMove> GetAvailableMoves() => AvailableMoves;

        public TestGameResult GetResult() => Result;

        public IGameState<GridMove, TestGameResult> Move(GridMove move)
        {
            return this;
        }
    }
}
