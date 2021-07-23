using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using Faze.Core.TreeMappers;
using Faze.Examples.Gallery.Services.Aggregates;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.Services.TreeMappers
{
    public class EightQueensProblemSolutionTreeMapper : ITreeMapper<IGameState<GridMove, SingleScoreResult?>, EightQueensProblemSolutionAggregate>
    {
        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree, IProgressTracker progress)
        {
            progress.SetMaxTicks(64);

            //var invariantMapper = new MoveOrderInvariantTreeMapper();
            //tree = invariantMapper.Map(tree, progress);

            return MapTreeAgg(tree, TreeMapInfo.Root(), progress);
        }

        private static Tree<EightQueensProblemSolutionAggregate> MapTreeAgg(Tree<IGameState<GridMove, SingleScoreResult?>> tree, TreeMapInfo info, IProgressTracker progress)
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

            var children = tree.Children.Select((x, i) => MapTreeAgg(x, info.Child(i), progress)).ToList();


            var value = new EightQueensProblemSolutionAggregate();
            
            foreach (var childValue in children.Where(x => x != null).Select(x => x.Value))
            {
                value.Add(childValue);
            }

            if (info.Depth == 1)
            {
                progress.Tick();
            } 
            else if (info.Depth == 2)
            {
                progress.SetMessage(string.Join(",", info.GetPath()));
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
