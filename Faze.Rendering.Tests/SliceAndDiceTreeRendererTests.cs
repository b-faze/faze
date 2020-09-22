using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using Xunit;

namespace Faze.Rendering.Tests
{
    public class SliceAndDiceTreeRendererTests
    {
        [Fact]
        public void Test1()
        {
            var rendererOptions = new SliceAndDiceTreeRendererOptions
            {
                BorderProportion = 0.1
            };
            var renderer = new SliceAndDiceTreeRenderer(rendererOptions);
            var tree = CreateTestTree();
            var filename = GetTestOutputPath($"Test1.png");

            using (var img = renderer.Draw(tree, 600))
            {
                img.Save(filename, ImageFormat.Png);
            }
            
        }

        private string GetTestOutputPath(string filename)
        {
            var directory = $"../../../TestOutputs/{nameof(SliceAndDiceTreeRendererTests)}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Path.Combine(directory, filename);
        }

        // needs more work
        private PaintedTree CreateIrregularTree(int size, Queue<int> sizeQueue, int maxDepth, int depth = 0) 
        {
            if (depth == maxDepth)
                return new PaintedTree(Color.Black);

            var children = new List<PaintedTree>();
            var sizes = new List<int>();
            for (var i = 0; i < size; i++)
            {
                if (sizeQueue.TryDequeue(out var childSize))
                    sizes.Add(childSize);
                else
                    sizes.Add(0);
            }

            foreach (var childSize in sizes)
            {
                children.Add(CreateIrregularTree(childSize, sizeQueue, maxDepth, depth + 1));
            }

            var grey = (int)(255 * (1 - (double)depth / maxDepth));
            return new PaintedTree(Color.FromArgb(grey, grey, grey), children);
        }

        private PaintedTree CreateTestTree()
        {
            var c1 = new PaintedTree(Color.Red);
            var c2 = new PaintedTree(Color.Blue);
            var c3 = new PaintedTree(Color.Green);
            var c4 = new PaintedTree(Color.Cyan);
            var c5 = new PaintedTree(Color.Chocolate);

            var c6 = new PaintedTree(Color.DarkViolet, new []{ c1, c2, c4 });
            var c7 = new PaintedTree(Color.Firebrick, new[] { c5 });

            return new PaintedTree(Color.Black, new[] { c6, c7 });
        }
    }
}
