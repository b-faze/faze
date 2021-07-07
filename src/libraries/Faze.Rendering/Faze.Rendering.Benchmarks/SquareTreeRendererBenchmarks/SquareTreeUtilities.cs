using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faze.Rendering.TreeLinq;
using System.Drawing;

namespace Faze.Rendering.Benchmarks.SquareTreeRendererBenchmarks
{
    public static class SquareTreeUtilities
    {
        public static Tree<Color> CreateGreyPaintedSquareTree(int size, int maxDepth, int depth = 0)
        {
            var tree = CreateSquareTree(size, maxDepth, depth)
                .MapValue((v, info) => info.Depth)
                .MapValue(v => (int)(255 * (1 - (double)v / maxDepth)))
                .MapValue(v => Color.FromArgb(v, v, v));

            return new Tree<Color>(tree.Value, tree.Children);
        }

        private static Tree<int> CreateSquareTree(int size, int maxDepth, int depth = 0)
        {
            if (depth == maxDepth)
                return new Tree<int>(depth);

            var children = Enumerable.Range(0, size * size).Select(i => CreateSquareTree(size, maxDepth, depth + 1));

            return new Tree<int>(depth, children);
        }
    }
}
