using BenchmarkDotNet.Running;
using Faze.Engine.Benchmarks.TreeLinqBenchmarks;
using System;

namespace Faze.Engine.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("R_HOME", @"G:\Program Files (x86)\R-4.1.0");
            BenchmarkRunner.Run<LinqMapValueBenchmarks>();
        }
    }
}
