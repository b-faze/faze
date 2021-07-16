using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.Tests.Utilities;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using Xunit;

namespace Faze.Rendering.Tests.RendererTests
{
    public class SliceAndDiceTreeRendererTests
    {
        [DebugOnlyFact]
        public void Test1()
        {
            var rendererOptions = new SliceAndDiceTreeRendererOptions
            {
                BorderProportion = 0.1f
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
        private Tree<Color> CreateIrregularTree(int size, Queue<int> sizeQueue, int maxDepth, int depth = 0) 
        {
            if (depth == maxDepth)
                return new Tree<Color>(Color.Black);

            var children = new List<Tree<Color>>();
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
            return new Tree<Color>(Color.FromArgb(grey, grey, grey), children);
        }

        private Tree<Color> CreateTestTree()
        {
            var c1 = new Tree<Color>(Color.Red);
            var c2 = new Tree<Color>(Color.Blue);
            var c3 = new Tree<Color>(Color.Green);
            var c4 = new Tree<Color>(Color.Cyan);
            var c5 = new Tree<Color>(Color.Chocolate);

            var c6 = new Tree<Color>(Color.DarkViolet, new []{ c1, c2, c4 });
            var c7 = new Tree<Color>(Color.Firebrick, new[] { c5 });

            return new Tree<Color>(Color.Black, new[] { c6, c7 });
        }
    }
}
