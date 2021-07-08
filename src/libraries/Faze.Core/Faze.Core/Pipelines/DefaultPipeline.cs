using Faze.Abstractions.Core;
using System;
using System.Text;

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

        public IReversePipelineBuilder<TRequired> Require<TRequired>(Action<TRequired> fn)
        {
            return new PipelineBuilder<TRequired>(fn);
        }
    }
}
