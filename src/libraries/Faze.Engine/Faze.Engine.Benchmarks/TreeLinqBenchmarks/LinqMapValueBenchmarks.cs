using BenchmarkDotNet.Attributes;
using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faze.Core.TreeLinq;

namespace Faze.Engine.Benchmarks.TreeLinqBenchmarks
{
    [MemoryDiagnoser]
    public class LinqMapValueBenchmarks
    {
        private Tree<int> tree;

        [Params(1, 5, 10)]
        public int Itterations;

        [GlobalSetup]
        public void Setup()
        {
            tree = TreeUtilities.CreateTree<int>(3, 5);
        }

        [Benchmark(Baseline = true)]
        public void Standard()
        {
            for (var i = 0; i < Itterations; i++)
            {
                tree = tree.MapValue(MapFunction);
            }

            tree.GetLeaves().ToList();
        }

        private int MapFunction(int x) => x++;
    }
}
