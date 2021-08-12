using Faze.Abstractions.Core;
using Faze.Abstractions.Engine;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class WinLoseDrawResultsTreeIterater : IIterater<Tree<IGameState<GridMove, WinLoseDrawResult?>>, Tree<WinLoseDrawResultAggregate>>
    {
        private readonly IGameSimulator gameSimulator;
        private readonly int leafSimulations;

        public WinLoseDrawResultsTreeIterater(IGameSimulator gameSimulator, int leafSimulations)
        {
            this.gameSimulator = gameSimulator;
            this.leafSimulations = leafSimulations;
        }
        public IEnumerable<Tree<WinLoseDrawResultAggregate>> GetEnumerable(Tree<IGameState<GridMove, WinLoseDrawResult?>> stateTree)
        {
            var resultTree = GetBaseResultTree(stateTree).Evaluate();

            for (var i = 0; i < leafSimulations; i++)
            {
                var results = GetResults(stateTree);
                foreach (var (path, result) in results)
                {
                    foreach (var node in resultTree.SelectPath(path))
                    {
                        node.Add(result);
                    }
                }

                yield return resultTree;
            }
        }

        private Tree<WinLoseDrawResultAggregate> GetBaseResultTree(Tree<IGameState<GridMove, WinLoseDrawResult?>> stateTree)
        {
            return stateTree.MapValue(state =>
            {
                var result = state.GetResult();

                return result.HasValue
                    ? new WinLoseDrawResultAggregate(result.Value)
                    : new WinLoseDrawResultAggregate();
            });
        }

        private IEnumerable<(int[] path, WinLoseDrawResult result)> GetResults(Tree<IGameState<GridMove, WinLoseDrawResult?>> stateTree) 
        {
            var leaves = stateTree
                .MapValue((state, info) => (state, info))
                .GetLeaves()
                .Select(t => t.Value);

            foreach (var (state, info) in leaves)
            {
                var result = state.GetResult() ?? gameSimulator.Simulate(state);
                if (result.HasValue)
                {
                    yield return (info.GetPath().ToArray(), result.Value);
                }
            }
        }
    }
}
