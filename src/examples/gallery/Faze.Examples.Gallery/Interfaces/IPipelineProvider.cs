using Faze.Abstractions.Core;
namespace Faze.Examples.Gallery.Interfaces
{
    public interface IPipelineProvider
    {
        IVisualisationPipeline GetPipeline(string id);
    }
}
