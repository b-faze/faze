using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Pipelines
{
    internal class DefaultPipeline : IPipeline
    {
        private IList<IPipelineStep> steps;

        internal DefaultPipeline(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        public void Run(IProgressTracker progress)
        {
            progress = progress ?? NullProgressTracker.Instance;

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
