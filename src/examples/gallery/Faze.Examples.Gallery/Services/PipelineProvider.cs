using Faze.Examples.Gallery.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.Services
{
    public class PipelineProvider : IPipelineProvider
    {
        private readonly Dictionary<string, IVisualisationPipeline> pipelines;

        public PipelineProvider(IEnumerable<IVisualisationPipeline> pipelines)
        {
            this.pipelines = pipelines.ToDictionary(x => x.GetId(), x => x);
        }

        public IVisualisationPipeline GetPipeline(string id)
        {
            if (!pipelines.TryGetValue(id, out var pipeline))
                return null;

            return pipeline;
        }
    }
}
