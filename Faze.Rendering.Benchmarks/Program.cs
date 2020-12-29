using BenchmarkDotNet.Running;
using Faze.Rendering.Benchmarks.RendererBenchmarks;
using System;

namespace Faze.Rendering.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DrawSquareTreeRendererBenchmarks>();
        }
    }
}
