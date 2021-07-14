﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Abstractions.Core
{
    public class DefaultPipeline<T> : IPipeline<T>
    {
        private IList<IPipelineStep> steps;

        public DefaultPipeline(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        public void Run(T input)
        {
            object currentInput = input;

            foreach (var step in steps)
            {
                currentInput = step.Execute(currentInput);
            }
        }

        public void Run(T input, IProgressBar progress)
        {
            progress.SetMaxTicks(steps.Count);

            object currentInput = input;

            foreach (var step in steps)
            {
                switch (step)
                {
                    case IPipelineStepProgress pipelineStepProgress:
                        using (var subprogress = progress.Spawn())
                        {
                            currentInput = pipelineStepProgress.Execute(currentInput, subprogress);
                        }
                        break;

                    default:
                        currentInput = step.Execute(currentInput);
                        break;
                }

                progress.Tick();
            }
        }
    }

    public class DefaultPipeline : IPipeline
    {
        private IList<IPipelineStep> steps;

        public DefaultPipeline(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        public void Run()
        {
            object currentInput = null;

            foreach (var step in steps)
            {
                currentInput = step.Execute(currentInput);
            }
        }

        public void Run(IProgressBar progress)
        {
            progress.SetMaxTicks(steps.Count);

            object currentInput = null;

            foreach (var step in steps)
            {
                switch (step)
                {
                    case IPipelineStepProgress pipelineStepProgress:
                        using (var subprogress = progress.Spawn())
                        {
                            currentInput = pipelineStepProgress.Execute(currentInput, subprogress);
                        }
                        break;

                    default:
                        currentInput = step.Execute(currentInput);
                        break;
                }

                progress.Tick();
            }
        }
    }
}