using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Linq;

namespace Faze.Examples.QuickStartConsole.V3
{
    public class MyCustomGameResultsMapper : ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, double>
    {
        public Tree<double> Map(Tree<IGameState<GridMove, WinLoseDrawResult?>> tree, IProgressTracker progress)
        {
            return tree
                .MapValue(state => state.GetResult())
                .MapTree(GetScore)
                .NormaliseSiblings();
        }

        private double GetScore(Tree<WinLoseDrawResult?> tree, TreeMapInfo info)
        {
            if (tree == null)
                return 0;

            if (tree.IsLeaf())
            {
                var result = tree.Value;
                double score = result == WinLoseDrawResult.Win ? 100 : 0;
                return score / (info.Depth + 1);
            }

            var childCount = tree.Children.Count();
            var childSum = tree.Children.Select((child, index) => GetScore(child, info.Child(index))).Sum();

            return childSum / childCount;
        }
    }
}
