using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Visualisations.EightQueensProblem;

namespace Faze.Examples.Gallery
{
    public interface IPipelineProvider
    {
        IVisualisationPipeline GetPipeline(string id);
    }
}
