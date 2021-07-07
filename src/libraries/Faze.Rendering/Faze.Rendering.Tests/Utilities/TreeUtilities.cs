using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeLinq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Faze.Rendering.Tests.Utilities
{
    public class TreeUtilities
    {
        public static Tree<Color> CreateGreyPaintedSquareTree(int size, int maxDepth, int depth = 0)
        {
            var tree = CreateSquareTree(size, maxDepth, depth)
                .MapValue((v, info) => info.Depth)
                .MapValue(v => (int)(255 * (1 - (double)v / maxDepth)))
                .MapValue(v => Color.FromArgb(v, v, v));

            return new Tree<Color>(tree.Value, tree.Children);
        }

        public static Tree<Color> CreateRainbowPaintedSquareTree(int size, int maxDepth, int depth = 0)
        {
            var tree = CreateSquareTree(size, maxDepth, depth);
            var nodeCount = tree.SelectDepthFirst().Count();
            var colorInterpolator = new TurboInterpolator();

            var i = 0;
            var coloredTree = tree
                .MapValue(v => i++)
                .MapValue(v => (double)v/nodeCount)
                .MapValue(colorInterpolator);

            return new Tree<Color>(coloredTree.Value, coloredTree.Children);
        }

        private static Color ToRainbowColor(int i, int size) 
        {
            var frequency = 2 * Math.PI / size;

            var r = (int)(Math.Sin(frequency * i + 0) * 127 + 128);
            var g = (int)(Math.Sin(frequency * i + 2) * 127 + 128);
            var b = (int)(Math.Sin(frequency * i + 4) * 127 + 128);
            
            return Color.FromArgb(r, g, b);
        }

        private static Color ToUniqueColor(int i)
        {
            var redFrequency = 1.666;
            var grnFrequency = 2.666;
            var bluFrequency = 3.666;

            var r = (int)(Math.Sin(redFrequency * i + 0) * 127 + 128);
            var g = (int)(Math.Sin(grnFrequency * i + 2) * 127 + 128);
            var b = (int)(Math.Sin(bluFrequency * i + 4) * 127 + 128);

            return Color.FromArgb(r, g, b);
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
