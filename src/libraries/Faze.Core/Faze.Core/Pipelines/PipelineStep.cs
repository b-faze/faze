using Faze.Abstractions.Core;
using System;

namespace Faze.Core.Pipelines
{
    public class PipelineStep<TIn, TOut> : IPipelineStep
    {
        public PipelineStep(Action<TIn> fn)
        {
            Fn = obj =>
            {
                fn(obj);
                return default(TOut);
            };
        }

        public PipelineStep(Func<TOut> fn)
        {
            Fn = obj => fn();
        }

        public PipelineStep(Func<TIn, TOut> fn)
        {
            Fn = fn;
        }

        public Func<TIn, TOut> Fn { get; }

        public object Execute(object input)
        {
            return Fn((TIn)input);
        }
    }
}
