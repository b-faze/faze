using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Pipelines
{
    internal class Pipeline<TInput, TOutput> : IPipeline, IPipeline<TInput>, IPipeline<TInput, TOutput>
    {
        private IList<IPipelineStep> steps;

        internal Pipeline(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        public void Run(IProgressTracker progress = null)
        {
            Run(progress);
        }

        public void Run(TInput input, IProgressTracker progress = null)
        {
            Run(progress, input);
        }

        TOutput IPipeline<TInput, TOutput>.Run(TInput input, IProgressTracker progress)
        {
            return (TOutput)Run(progress, input);
        }

        private object Run(IProgressTracker progress, object input = null)
        {
            progress = progress ?? NullProgressTracker.Instance;

            progress.SetMaxTicks(steps.Count);

            foreach (var step in steps)
            {
                switch (step)
                {
                    case IPipelineStepProgress pipelineStepProgress:
                        using (var subprogress = progress.Spawn())
                        {
                            input = pipelineStepProgress.Execute(input, subprogress);
                        }
                        break;

                    default:
                        input = step.Execute(input);
                        break;
                }

                progress.Tick();
            }

            return input;
        }
    }
    //internal class DefaultPipeline : IPipeline
    //{
    //    private Pipeline pipeline;

    //    internal DefaultPipeline(Pipeline pipeline)
    //    {
    //        this.pipeline = pipeline;
    //    }

    //    public void Run(IProgressTracker progress)
    //    {
    //        pipeline.Run(progress);
    //    }
    //}
}
