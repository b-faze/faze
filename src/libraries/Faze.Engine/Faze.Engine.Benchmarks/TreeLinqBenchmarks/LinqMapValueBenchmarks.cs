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
    [RPlotExporter]
    public class LinqMapValueBenchmarks
    {
        private Tree<int> tree;

        [Params(1, 2, 3)]
        public int Depth;

        [GlobalSetup]
        public void Setup()
        {
            tree = TreeUtilities.CreateTree<int>(3, Depth);
        }

        [Benchmark(Baseline = true)]
        public void Standard()
        {
            var mappedTree = tree
                .MapValue(x => Map(Map(Map(x))));

            EnumerateTree(mappedTree);
        }

        [Benchmark]
        public void Multiple()
        {
            var mappedTree = tree
                .MapValue(Map)
                .MapValue(Map)
                .MapValue(Map);

            EnumerateTree(mappedTree);
        }

        private static int Map(int x) => x + 1;

        private static void EnumerateTree<T>(Tree<T> tree)
        {
            foreach (var leaf in tree.GetLeaves())
            {
                // do nothing
            }
        }
    }
}
