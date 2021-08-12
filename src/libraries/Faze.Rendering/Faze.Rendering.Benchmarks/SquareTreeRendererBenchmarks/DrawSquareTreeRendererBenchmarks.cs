using BenchmarkDotNet.Attributes;
using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.Benchmarks.SquareTreeRendererBenchmarks;
using Faze.Rendering.Benchmarks.SquareTreeRendererBenchmarks.Renderers;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Faze.Rendering.Benchmarks.RendererBenchmarks
{
    [MemoryDiagnoser]
    public class DrawSquareTreeRendererBenchmarks
    {
        private Tree<Color> tree;

        [Params(1, 3, 4, 10)]
        public int TreeSize;

        [Params(100, 500, 1000, 2000)]
        public int ImageSize;

        [GlobalSetup]
        public void Setup()
        {
            tree = SquareTreeUtilities.CreateGreyPaintedSquareTree(TreeSize, 10);
        }

        [Benchmark(Baseline = true)]
        public void Standard()
        {
            var options = new SquareTreeRendererOptions(TreeSize, ImageSize)
            {
                BorderProportion = 0
            };

            var renderer = new StandardSquareTreeRenderer(options);

            renderer.Draw(tree);

            using (var ms = new MemoryStream())
            {
                renderer.WriteToStream(ms);
                var img = Image.FromStream(ms);
            }

        }

        [Benchmark]
        public void Skia()
        {
            var options = new SquareTreeRendererOptions(TreeSize, ImageSize)
            {
                BorderProportion = 0
            };

            var renderer = new SkiaSquareTreeRenderer(options);

            renderer.Draw(tree);

            using (var ms = new MemoryStream())
            {
                renderer.WriteToStream(ms);
                var img = Image.FromStream(ms);
            }
        }
    }
}
