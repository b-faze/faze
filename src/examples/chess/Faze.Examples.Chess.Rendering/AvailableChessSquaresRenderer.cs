using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Faze.Core.TreeLinq;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Faze.Rendering.Extensions;
using Faze.Core.Extensions;

namespace Faze.Games.Chess.Rendering
{
    public class AvailableChessSquaresRenderer
    {
        public void Draw(IGameState<ChessMove, ChessResult> state, int maxDepth, string filename)
        {
            var options = new SquareTreeRendererOptions(8, 1000)
            {
                BorderProportions = 0
            };
            var renderer = new SquareTreeRenderer(options);
            var tree = GetTree(state, maxDepth);


            renderer.Draw(tree);

            renderer.Save(filename);
        }

        private Tree<Color> GetTree(IGameState<ChessMove, ChessResult> state, int maxDepth) 
        {
            var tree = Limit(state.ToPathTree()
                .MapValue(path => path.Select(ToGridIndex).ToArray()), maxDepth);

            var leaves = tree.GetLeaves().Select(x => x.Value).ToArray();
            var countTree = GetTileMoveCounts(leaves);
            var normalisedTree = NormaliseTree(countTree, countTree.Value);
            var colorTree = normalisedTree.MapValue(new LinearColorInterpolator(Color.Blue, Color.Red));

            return colorTree;
        }

        private Tree<double> NormaliseTree(Tree<int> tree, int siblingMaxValue)
        {
            if (tree == null)
                return null;

            var value = (double)tree.Value / siblingMaxValue;
            if (tree.Children == null || !tree.Children.Any())
                return new Tree<double>(value);

            var maxChildrenValue = tree.Children.Select(x => x?.Value ?? 0).Max();
            var children = tree.Children.Select(x => NormaliseTree(x, maxChildrenValue));

            return new Tree<double>(value, children);
        }

        private Tree<int> GetTileMoveCounts(IEnumerable<int[]> paths) 
        {
            if (!paths.Any() || paths.All(p => p.Length == 0))
                return new Tree<int>(1);

            var value = 0;
            var children = Enumerable.Range(0, 64).Select(x => new Tree<int>(0)).ToArray();
            var groups = paths.GroupBy(path => path.First()).ToArray();
            foreach (var group in groups)
            {
                var child = GetTileMoveCounts(group.Select(path => path.Skip(1).ToArray()));
                children[group.Key] = child;
                value += child.Value;
            }

            return new Tree<int>(value, children);
        }

        private int ToGridIndex(ChessMove chessMove)
        {
            return (int)chessMove.ToSquare();
        }

        private Tree<TValue> Limit<TValue>(Tree<TValue> tree, int maxDepth, int currentDepth = 0)
        {
            if (currentDepth >= maxDepth)
            {
                return new Tree<TValue>(tree.Value);
            }

            var children = tree.Children?.Select(x => Limit(x, maxDepth, currentDepth + 1));
            return new Tree<TValue>(tree.Value, children);
        }
    }
}
