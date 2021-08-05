using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.TreeLinq;
using Faze.Core.TreeMappers;
using Faze.Examples.Gallery.Services.Aggregates;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.Services.TreeMappers
{
    public class EightQueensProblemSolutionTreeMapper : ITreeMapper<IGameState<GridMove, SingleScoreResult?>, EightQueensProblemSolutionAggregate>
    {
        private readonly int evaluationDepth;

        public EightQueensProblemSolutionTreeMapper(int evaluationDepth)
        {
            this.evaluationDepth = evaluationDepth;
        }

        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree, IProgressTracker progress)
        {
            return MapTreeAgg(tree, TreeMapInfo.Root(), progress);
        }

        private Tree<EightQueensProblemSolutionAggregate> MapTreeAgg(Tree<IGameState<GridMove, SingleScoreResult?>> tree, TreeMapInfo info, IProgressTracker progress)
        {
            if (info.Depth <= 3)
                progress.SetMessage(string.Join(",", info.GetPath()));

            if (tree == null)
            {
                return null;
            }

            if (tree.IsLeaf() || info.Depth == evaluationDepth)
            {
                var resultAgg = AggregateResults(tree);

                return new Tree<EightQueensProblemSolutionAggregate>(resultAgg);
            }

            var value = new EightQueensProblemSolutionAggregate();
            var children = tree.Children.Select((x, i) => MapTreeAgg(x, info.Child(i), progress)).ToList();

            return new Tree<EightQueensProblemSolutionAggregate>(value, children);

        }

        private static EightQueensProblemSolutionAggregate AggregateResults(Tree<IGameState<GridMove, SingleScoreResult?>> stateTree)
        {
            var gameResults = stateTree
                .GetLeaves()
                .Select(x => x.Value.GetResult())
                .Where(x => x != null);

            var aggResult = new EightQueensProblemSolutionAggregate();
            foreach (var result in gameResults)
            {
                aggResult.Add(result.Value);
            }

            return aggResult;
        }
    }
}
