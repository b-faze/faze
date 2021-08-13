using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.Video.Extensions;

namespace Faze.Examples.Gallery.Extensions
{
    public static class ReversePipelineExtensions
    {
        public static IReversePipelineBuilder<IStreamer> GalleryImage(this IReversePipelineBuilder builder, IGalleryService galleryService, GalleryItemMetadata data)
        {
            return builder.Require<IStreamer>(renderer => galleryService.Save(renderer, data));
        }

        public static IReversePipelineBuilder<IStreamer> GalleryVideo(this IReversePipelineBuilder builder, IGalleryService galleryService, GalleryItemMetadata data)
        {
            var filename = galleryService.GetItemFilename(data);
            return builder
                .Video(filename, new VideoFFMPEGSettings(24));
        }
    }
}
