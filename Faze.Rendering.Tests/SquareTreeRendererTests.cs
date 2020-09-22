using Faze.Abstractions.Rendering;
using Faze.Rendering.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xunit;

namespace Faze.Rendering.Tests
{
    public class SquareTreeRendererTests
    {
        [Theory]
        [InlineData(1, 1, 0.1)]
        [InlineData(1, 2, 0.1)]
        [InlineData(1, 3, 0.1)]
        [InlineData(1, 4, 0.1)]
        [InlineData(2, 1, 0.1)]
        [InlineData(2, 2, 0.1)]
        [InlineData(2, 3, 0.1)]
        [InlineData(2, 4, 0.1)]
        [InlineData(3, 1, 0.1)]
        [InlineData(3, 2, 0.1)]
        [InlineData(3, 3, 0.1)]
        [InlineData(3, 4, 0.1)]
        public void Test1(int squareSize, int depth, double borderProportion)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize)
            {
                BorderProportions = borderProportion,
                IgnoreRootNode = true
            };
            var renderer = new SquareTreeRenderer(rendererOptions);
            var tree = CreateSquareTree(squareSize, depth);
            var filename = GetTestOutputPath($"Test1_{squareSize}_{depth}_{borderProportion}.png");

            using (var img = renderer.Draw(tree, 600))
            {
                img.Save(filename, ImageFormat.Png);
            }
            
        }

        private string GetTestOutputPath(string filename)
        {
            var directory = "../../../TestOutputs/SquareTreeRendererTests";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Path.Combine(directory, filename);
        }

        private PaintedTree CreateSquareTree(int size, int maxDepth, int depth = 0) 
        {
            if (depth == maxDepth)
                return new PaintedTree(Color.Black);

            var children = new List<PaintedTree>();
            for (var i = 0; i < size * size; i++)
            {
                children.Add(CreateSquareTree(size, maxDepth, depth + 1));
            }

            var grey = (int)(255 * (1 - (double)depth / maxDepth));
            return new PaintedTree(Color.FromArgb(grey, grey, grey), children);
        }
    }
}
