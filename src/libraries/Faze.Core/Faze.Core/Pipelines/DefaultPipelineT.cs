using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faze.Core.Pipelines
{
    public class DefaultPipeline<T> : IPipeline<T>
    {
        private IList<IPipelineStep> steps;

        internal DefaultPipeline(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        public void Run(T input, IProgressTracker progress)
        {
            progress = progress ?? NullProgressTracker.Instance;

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
}
