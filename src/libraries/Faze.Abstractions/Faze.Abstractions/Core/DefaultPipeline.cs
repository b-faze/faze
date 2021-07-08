using System;
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
    }
}
