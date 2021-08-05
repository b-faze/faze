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

        public Tree<EightQueensProblemSolutionAggregate> Map(Tree<IGameState<GridMove, SingleScoreResult?>> tree, IProgressTracker progress)
        {
            var results = GetAllResults2(tree, progress);

            return results;
            //return MapTreeAgg(tree, TreeMapInfo.Root(), null, progress);
        }

        private Tree<EightQueensProblemSolutionAggregate> MapTreeAgg(Tree<IGameState<GridMove, SingleScoreResult?>> tree, TreeMapInfo info, IDictionary<string, EightQueensProblemSolutionAggregate> dict, IProgressTracker progress)
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
            var children = tree.Children.Select((x, i) => MapTreeAgg(x, info.Child(i), dict, progress)).ToList();

            return new Tree<EightQueensProblemSolutionAggregate>(value, children);

        }

        private Tree<EightQueensProblemSolutionAggregate> GetAllResults(Tree<IGameState<GridMove, SingleScoreResult?>> stateTree, IProgressTracker progress)
        {
            var results = new MoveOrderInvariantTreeMapper2().Map(stateTree, progress)
                .MapValue((v, info) => (v.GetResult(), info.GetPath().ToArray()))
                .GetLeaves()
                .Select(t => t.Value);

            var resultTree = new AddTree<EightQueensProblemSolutionAggregate>(new AddTreeConfig<EightQueensProblemSolutionAggregate>
            {
                AddValueFn = (existing, newValue) =>
                {
                    if (existing == null)
                        existing = new EightQueensProblemSolutionAggregate();

                    existing.Add(newValue);
                    return existing;
                },
                AddWhileTraversing = true
            });

            foreach (var (result, basePath) in results) 
            {
                if (result == null) continue;

                var value = new EightQueensProblemSolutionAggregate(result.Value);
                var paths = Choose(basePath, evaluationDepth);
                foreach (var path in paths)
                {
                    resultTree.Add(path, value);
                }

            }

            return resultTree;
        }

        private Tree<EightQueensProblemSolutionAggregate> GetAllResults2(Tree<IGameState<GridMove, SingleScoreResult?>> stateTree, IProgressTracker progress)
        {
            var results = new MoveOrderInvariantTreeMapper2().Map(stateTree, progress)
                .MapValue((v, info) => (v.GetResult(), info.GetPath().ToArray()))
                .GetLeaves()
                .Select(t => t.Value);

            var tree = stateTree
                .LimitDepth(3)
                .MapValue(v => new EightQueensProblemSolutionAggregate())
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


        private IEnumerable<T[]> Choose<T>(T[] ts, int n)
        {
            if (ts.Length <= n)
            {
                return new[] { ts };
            }

            return ChooseHelper(ts, n);

        }

        private IEnumerable<T[]> ChooseHelper<T>(T[] ts, int n)
        {
            if (ts.Length == n)
            {
                yield return ts;
            }

            if (n == 0)
            {
                yield return new T[0];
                yield break;
            }

            for (var i = 0; i < ts.Length; i++)
            {
                var newTs = ts.Skip(i + 1).ToArray();
                var chosen = ts[i];
                foreach (var result in Choose(newTs, n - 1))
                {
                    yield return new[] { chosen }.Concat(result).ToArray();
                }

            }

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
