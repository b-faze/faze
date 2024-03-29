﻿using Faze.Abstractions.Core;
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
            RunInternal(progress);
        }

        public void Run(TInput input, IProgressTracker progress = null)
        {
            RunInternal(progress, input);
        }

        TOutput IPipeline<TInput, TOutput>.Run(TInput input, IProgressTracker progress)
        {
            return (TOutput)RunInternal(progress, input);
        }

        private object RunInternal(IProgressTracker progress, object input = null)
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
}
