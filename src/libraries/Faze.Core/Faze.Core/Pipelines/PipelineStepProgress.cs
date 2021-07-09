using Faze.Abstractions.Core;
using System;

namespace Faze.Core.Pipelines
{
    public class PipelineStepProgress<TIn, TOut> : IPipelineStepProgress
    {
        public PipelineStepProgress(Action<TIn, IProgressBar> fn)
        {
            Fn = (obj, progress) =>
            {
                fn(obj, progress);
                return default(TOut);
            };
        }

        public PipelineStepProgress(Func<IProgressBar, TOut> fn)
        {
            Fn = (_, progress) => fn(progress);
        }

        public PipelineStepProgress(Func<TIn, IProgressBar, TOut> fn)
        {
            Fn = fn;
        }

        public Func<TIn, IProgressBar, TOut> Fn { get; }
        public object Execute(object input, IProgressBar progress)
        {
            return Fn((TIn)input, progress);
        }

        public object Execute(object input)
        {
            return Fn((TIn)input, null);
        }
    }
}
