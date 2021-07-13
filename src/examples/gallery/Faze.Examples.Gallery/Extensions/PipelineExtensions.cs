using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;

namespace Faze.Examples.Gallery
{
    public static class PipelineExtensions
    {
        public static IReversePipelineBuilder<IPaintedTreeRenderer> GallerySave(this IReversePipelineBuilder builder, IGalleryService galleryService, GalleryItemMetadata data)
        {
            return builder.Require<IPaintedTreeRenderer>(renderer => galleryService.Save(renderer, data));
        }
    }
}
