using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Pipelines
{
    internal class ReversePipelineBuilder<T> : BasePipelineBuilder, IReversePipelineBuilder<T>
    {
        public ReversePipelineBuilder(Action<T> fn)
        {
            Add(new PipelineStep<T, object>(fn));
        }

        public ReversePipelineBuilder(Action<T, IProgressTracker> fn)
        {
            Add(new PipelineStepProgress<T, object>(fn));
        }

        protected ReversePipelineBuilder(IEnumerable<IPipelineStep> steps) : base(steps) 
        {
        }

        public virtual IReversePipelineBuilder<TRequired> Require<TRequired>(Func<TRequired, T> fn)
        {
            IPipelineStep newStep = new PipelineStep<TRequired, T>(fn);
            return new ReversePipelineBuilder<TRequired>(Steps.Concat(new[] { newStep }));
        }

        public virtual IReversePipelineBuilder<TRequired> Require<TRequired>(Func<TRequired, IProgressTracker, T> fn)
        {
            IPipelineStep newStep = new PipelineStepProgress<TRequired, T>(fn);
            return new ReversePipelineBuilder<TRequired>(Steps.Concat(new[] { newStep }));
        }

        public IPipeline<T> Build()
        {
            return new DefaultPipeline<T>(Steps.Reverse());
        }

        public IPipeline Build(Func<T> fn)
        {
            IPipelineStep newStep = new PipelineStep<object, T>(fn);
            return new DefaultPipeline(Steps.Concat(new[] { newStep }).Reverse());
        }
    }
}
