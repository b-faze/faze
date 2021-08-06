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
using System;
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

        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> stateTree, IProgressTracker progress)
        {
            var results = new CommutativePathTreePruner().Map(stateTree, progress)
                .MapValue((v, info) => (v.GetResult(), info.GetPath().ToArray()))
                .GetLeaves()
                .Select(t => t.Value);

            var tree = new CommutativePathTreePruner().Map(stateTree, NullProgressTracker.Instance)
                .MapValue(v => new EightQueensProblemSolutionAggregate())
                .LimitDepth(evaluationDepth)
                .Evaluate();

            foreach (var (result, path) in results)
            {
                if (result == null)
                    continue;

                var pathPermutations = Permutate(path);
                foreach (var pathPermutation in pathPermutations)
                {
                    foreach (var aggResult in tree.SelectPath(pathPermutation))
                    {
                        aggResult.Add(result.Value);
                    }
                }

            }

            return tree;
        }

        private IEnumerable<T[]> Permutate<T>(T[] ts)
        {
            if (ts.Length <= 1)
            {
                yield return ts;
                yield break;
            }

            for (var i = 0; i < ts.Length; i++)
            {
                var newTs = ts.Take(i).Concat(ts.Skip(i + 1)).ToArray();
                var chosen = ts[i];
                foreach (var result in Permutate(newTs))
                {
                    yield return new[] { chosen }.Concat(result).ToArray();
                }
            }
        }
    }
}
