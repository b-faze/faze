using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.QuickStartConsole.V2
{
    public class MyCustomGame : IGameState<GridMove, WinLoseDrawResult?>
    {
        private int target;
        private int currentValue;

        public static MyCustomGame Initial(int target)
        {
            return new MyCustomGame(target, 0);
        }

        private MyCustomGame(int target, int currentValue)
        {
            this.target = target;
            this.currentValue = currentValue;
        }

        public PlayerIndex CurrentPlayerIndex => PlayerIndex.P1;

        public IEnumerable<GridMove> GetAvailableMoves()
        {
            if (GetResult() != null)
            {
                return new GridMove[0];
            }

            return Enumerable.Range(0, 9).Select(i => new GridMove(i));
        }

        public WinLoseDrawResult? GetResult()
        {
            if (currentValue < target)
                return null;

            if (currentValue == target)
                return WinLoseDrawResult.Win;

            return WinLoseDrawResult.Lose;
        }

        public IGameState<GridMove, WinLoseDrawResult?> Move(GridMove move)
        {
            var moveValue = move + 1; // move is zero-indexed
            return new MyCustomGame(target, currentValue + moveValue);
        }
    }
}
