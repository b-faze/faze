using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Pipelines
{
    internal abstract class BasePipelineBuilder
    {
        private readonly IList<IPipelineStep> steps;

        public BasePipelineBuilder()
        {
            steps = new List<IPipelineStep>();
        }

        protected BasePipelineBuilder(IEnumerable<IPipelineStep> steps)
        {
            this.steps = steps.ToList();
        }

        protected IEnumerable<IPipelineStep> Steps => steps;

        protected void Add(IPipelineStep step) => steps.Add(step);
    }
}
