using BenchmarkDotNet.Attributes;
using Faze.Abstractions.Rendering;
using Faze.Rendering.Benchmarks.SquareTreeRendererBenchmarks;
using Faze.Rendering.TreeRenderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Rendering.Benchmarks.RendererBenchmarks
{
    [MemoryDiagnoser]
    public class DrawSquareTreeRendererBenchmarks
    {
        private PaintedTree tree;

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
            var options = new SquareTreeRendererOptions(TreeSize)
            {
                BorderProportions = 0
            };

            var renderer = new StandardSquareTreeRenderer(options);

            var result = renderer.Draw(tree, ImageSize);
        }

        [Benchmark]
        public void Skia()
        {
            var options = new SquareTreeRendererOptions(TreeSize)
            {
                BorderProportions = 0
            };

            var renderer = new SkiaSquareTreeRenderer(options);

            var result = renderer.Draw(tree, ImageSize);
        }
    }
}
