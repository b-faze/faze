using BenchmarkDotNet.Running;
using Faze.Engine.Benchmarks.TreeLinqBenchmarks;
using System;

namespace Faze.Engine.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<LinqMapValueBenchmarks>();
        }
    }
}
