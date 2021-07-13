using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.CLI.Visualisations.PieceBoards.DataGenerators
{
    public class EightQueensProblemSolutionTreeMapper : ITreeMapper<IGameState<GridMove, SingleScoreResult?>, EightQueensProblemSolutionAggregate>
    {
        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree)
        {
            return MapTreeAgg(tree, 0, null);
        }

        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree, IProgressBar progress)
        {
            return MapTreeAgg(tree, 0, progress);
        }

        private static Tree<EightQueensProblemSolutionAggregate> MapTreeAgg(Tree<IGameState<GridMove, SingleScoreResult?>> tree, int depth, IProgressBar progress)
        {
            if (tree == null)
            {
                progress.Tick();
                return null;
            }


            if (tree.IsLeaf())
            {
                var resultAgg = AggregateResults(tree.Value);

                progress.Tick();
                return new Tree<EightQueensProblemSolutionAggregate>(resultAgg);
            }

            using var progressChild = progress.Spawn();

            progressChild.SetMessage((depth + 1).ToString());
            progressChild.SetMaxTicks(64);

            var children = tree.Children.Select(x => MapTreeAgg(x, depth + 1, progressChild));

            var value = children
                .Where(x => x != null)
                .Select(x => x.Value)
                .Aggregate((a, b) =>
                {
                    a.Value.Add(b.Value);
                    return a.Value;
                });

            return new Tree<EightQueensProblemSolutionAggregate>(value, children);

        }

        private static EightQueensProblemSolutionAggregate AggregateResults(IGameState<GridMove, SingleScoreResult?> state)
        {
            var resultAgg = new EightQueensProblemSolutionAggregate();
            foreach (var result in CollectResults(state))
            {
                resultAgg.Add(result);
            }

            return resultAgg;
        }

        private static IEnumerable<SingleScoreResult> CollectResults(IGameState<GridMove, SingleScoreResult?> state) 
        {
            var result = state.GetResult();
            if (result != null) 
            {
                yield return result.Value;
            }

            foreach (var move in state.GetAvailableMoves())
            {
                foreach (var childResult in CollectResults(state.Move(move)))
                {
                    yield return childResult;
                }
            }
        }
    }
}
