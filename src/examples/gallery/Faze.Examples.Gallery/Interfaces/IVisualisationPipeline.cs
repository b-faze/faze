using Faze.Abstractions.Core;

namespace Faze.Examples.Gallery.Interfaces
{
    public interface IVisualisationPipeline
    {
        string Id { get; }
        string DataId { get; }
        IPipeline Create(GalleryItemMetadata galleryMetaData);
    }

    public interface IVisualisationPipeline<TConfig> : IVisualisationPipeline
    {
        IPipeline Create(GalleryItemMetadata<TConfig> galleryMetaData);
    }
}
