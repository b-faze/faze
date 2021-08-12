using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Interfaces
{
    public interface IImageGenerator
    {
        Task Generate(IProgressTracker progress);
        ImageGeneratorMetaData GetMetaData();
    }

    public interface IImageGenerator2
    {
        IEnumerable<GalleryItemMetadata> GetMetaData();
    }
}
