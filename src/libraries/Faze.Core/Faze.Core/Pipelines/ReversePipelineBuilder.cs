using Faze.Abstractions.Core;
using System;

namespace Faze.Core.Pipelines
{
    /// <summary>
    /// Allows for building up a pipeline in reverse order. 
    /// Starting with defining what you need
    /// </summary>
    public class ReversePipelineBuilder : IReversePipelineBuilder
    {
        public static IReversePipelineBuilder Create()
        {
            return new ReversePipelineBuilder();
        }

        public IReversePipelineBuilder<T, T> Require<T>()
        {
            return new ReversePipelineBuilder<T, T>(input => input);
        }

        public IReversePipelineBuilder<TRequired> Require<TRequired>(Action<TRequired> fn)
        {
            return new ReversePipelineBuilder<TRequired>(fn);
        }

        public IReversePipelineBuilder<TRequired> Require<TRequired>(Action<TRequired, IProgressTracker> fn)
        {
            return new ReversePipelineBuilder<TRequired>(fn);
        }
    }
}
