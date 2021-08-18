using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Pipelines
{
    internal class ReversePipelineBuilder<TOut, TIn> : BasePipelineBuilder, IReversePipelineBuilder<TOut, TIn>, IReversePipelineBuilder<TIn>
    {
        public ReversePipelineBuilder(Func<TIn, TOut> fn)
        {
            Add(new PipelineStep<TIn, TOut>(fn));
        }

        protected ReversePipelineBuilder(IEnumerable<IPipelineStep> steps) : base(steps)
        {
        }

        public IReversePipelineBuilder<TOut, TNext> Require<TNext>(Func<TNext, TIn> fn)
        {
            IPipelineStep newStep = new PipelineStep<TNext, TIn>(fn);
            return new ReversePipelineBuilder<TOut, TNext>(Steps.Concat(new[] { newStep }).ToList());
        }

        public IReversePipelineBuilder<TOut, TNext> Require<TNext>(Func<TNext, IProgressTracker, TIn> fn)
        {
            IPipelineStep newStep = new PipelineStepProgress<TNext, TIn>(fn);
            return new ReversePipelineBuilder<TOut, TNext>(Steps.Concat(new[] { newStep }).ToList());
        }

        public IPipeline<TIn, TOut> Build()
        {
            return new Pipeline<TIn, TOut>(Steps.Reverse());
        }

        public IPipeline<TOut> Build(Func<TIn> fn)
        {
            IPipelineStep newStep = new PipelineStep<object, TIn>(fn);
            return new Pipeline<TOut, object>(Steps.Concat(new[] { newStep }).Reverse());
        }

        #region  IReversePipelineBuilder<TNext> implementations

        IReversePipelineBuilder<TNext> IReversePipelineBuilder<TIn>.Require<TNext>(Func<TNext, TIn> fn)
        {
            return (IReversePipelineBuilder<TNext>)Require(fn);
        }

        IReversePipelineBuilder<TNext> IReversePipelineBuilder<TIn>.Require<TNext>(Func<TNext, IProgressTracker, TIn> fn)
        {
            return (IReversePipelineBuilder<TNext>)Require(fn);
        }

        IPipeline<TIn> IReversePipelineBuilder<TIn>.Build()
        {
            throw new NotImplementedException();
        }

        IPipeline IReversePipelineBuilder<TIn>.Build(Func<TIn> fn)
        {
            throw new NotImplementedException();
        }

        #endregion IReversePipelineBuilder<TNext> implementations


    }
}
