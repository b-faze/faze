using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Utilities.Testing
{
    public class TestTreeGameState<TResult> : IGameState<int, TResult>
    {
        private readonly Tree<TResult> gameTree;

        public TestTreeGameState(Tree<TResult> gameTree)
        {
            this.gameTree = gameTree;
        }

        public PlayerIndex CurrentPlayerIndex { get; set; }

        public IEnumerable<int> GetAvailableMoves() 
        {
            if (gameTree.IsLeaf())
                return new int[0];

            return Enumerable.Range(0, gameTree.Children.Count());
        }

        public TResult GetResult() => gameTree.Value;

        public IGameState<int, TResult> Move(int move)
        {
            return new TestTreeGameState<TResult>(gameTree.Children.ElementAt(move));
        }
    }
}
