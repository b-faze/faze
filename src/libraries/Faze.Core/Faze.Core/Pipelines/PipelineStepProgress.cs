using Faze.Abstractions.Core;
using System;

namespace Faze.Core.Pipelines
{
    public class PipelineStepProgress<TIn, TOut> : IPipelineStepProgress
    {
        public PipelineStepProgress(Action<TIn, IProgressTracker> fn)
        {
            Fn = (obj, progress) =>
            {
                fn(obj, progress);
                return default(TOut);
            };
        }

        public PipelineStepProgress(Func<IProgressTracker, TOut> fn)
        {
            Fn = (_, progress) => fn(progress);
        }

        public PipelineStepProgress(Func<TIn, IProgressTracker, TOut> fn)
        {
            Fn = fn;
        }

        public Func<TIn, IProgressTracker, TOut> Fn { get; }
        public object Execute(object input, IProgressTracker progress)
        {
            return Fn((TIn)input, progress);
        }

        public object Execute(object input)
        {
            return Fn((TIn)input, null);
        }
    }
}
