using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using Faze.Examples.Gallery.Services.Aggregates;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.Services.TreeMappers
{
    public class EightQueensProblemSolutionTreeMapper : ITreeMapper<IGameState<GridMove, SingleScoreResult?>, EightQueensProblemSolutionAggregate>
    {
        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree)
        {
            return MapTreeAgg(tree, new int[0], 0, null);
        }

        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree, IProgressBar progress)
        {
            progress.SetMaxTicks(64);
            return MapTreeAgg(tree, new int[0], 0, progress);
        }

        private static Tree<EightQueensProblemSolutionAggregate> MapTreeAgg(Tree<IGameState<GridMove, SingleScoreResult?>> tree, int[] path, int depth, IProgressBar progress)
        {
            if (tree == null)
            {
                return null;
            }


            if (tree.IsLeaf())
            {
                var resultAgg = AggregateResults(tree.Value);

                return new Tree<EightQueensProblemSolutionAggregate>(resultAgg);
            }

            var children = tree.Children.Select((x, i) => MapTreeAgg(x, path.Concat(new[] { i }).ToArray(), depth + 1, progress)).ToList();


            var value = new EightQueensProblemSolutionAggregate();
            
            foreach (var childValue in children.Where(x => x != null).Select(x => x.Value))
            {
                value.Add(childValue);
            }

            if (depth == 1)
            {
                progress.Tick();
            } 
            else if (depth == 2)
            {
                progress.SetMessage(string.Join(",", path));
            }

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
