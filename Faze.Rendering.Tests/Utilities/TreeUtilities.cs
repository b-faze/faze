using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Rendering.Tests.Utilities
{
    public class TreeUtilities
    {
        public static PaintedTree CreatePaintedSquareTree(int size, int maxDepth, int depth = 0)
        {
            if (depth == maxDepth)
                return new PaintedTree(Color.Black);

            var children = new List<PaintedTree>();
            for (var i = 0; i < size * size; i++)
            {
                children.Add(CreatePaintedSquareTree(size, maxDepth, depth + 1));
            }

            var grey = (int)(255 * (1 - (double)depth / maxDepth));
            return new PaintedTree(Color.FromArgb(grey, grey, grey), children);
        }

        public static Tree<int> CreateSquareTree(int size, int maxDepth, int depth = 0)
        {
            if (depth == maxDepth)
                return new Tree<int>(depth);

            var children = new List<Tree<int>>();
            for (var i = 0; i < size * size; i++)
            {
                children.Add(CreateSquareTree(size, maxDepth, depth + 1));
            }

            return new Tree<int>(depth, children);
        }
    }
}
