using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Pipelines
{
    public class PipelineBuilder<T> : IReversePipelineBuilder<T>
    {
        private readonly IList<IPipelineStep> steps;

        public PipelineBuilder(Action<T> fn)
        {
            this.steps = new[] { new PipelineStep<T, object>(fn) };
        }

        private PipelineBuilder(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        public IReversePipelineBuilder<TRequired> Require<TRequired>(Func<TRequired, T> fn)
        {
            IPipelineStep newStep = new PipelineStep<TRequired, T>(fn);
            return new PipelineBuilder<TRequired>(steps.Concat(new[] { newStep }).ToList());
        }

        public IPipeline<T> Build()
        {
            return new DefaultPipeline<T>(steps.Reverse());
        }
    }
}
