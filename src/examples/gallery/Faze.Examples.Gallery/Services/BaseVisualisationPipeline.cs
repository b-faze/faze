using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using System;

namespace Faze.Examples.Gallery.Services
{
    public abstract class BaseVisualisationPipeline<TConfig> : IVisualisationPipeline<TConfig>
    {
        public abstract string GetId();
        public abstract string GetDataId();

        public abstract IPipeline Create(GalleryItemMetadata<TConfig> galleryMetadata);

        public IPipeline Create(GalleryItemMetadata galleryMetadata)
        {
            if (galleryMetadata is GalleryItemMetadata<TConfig> typedMetaData)
            {
                return Create(typedMetaData);
            }

            throw new NotSupportedException($"'{nameof(galleryMetadata)}' must be of generic type '{typeof(TConfig)}'");

        }
    }
}
