using Faze.Abstractions.Core;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public interface IVisualisationPipeline
    {
        string Id { get; }
        IPipeline Create(GalleryItemMetadata galleryMetaData);
    }

    public interface IVisualisationPipeline<TConfig> : IVisualisationPipeline
    {
        IPipeline Create(GalleryItemMetadata<TConfig> galleryMetaData);
    }
}
