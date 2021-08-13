using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System.IO;

namespace Faze.Examples.Gallery.Interfaces
{
    public interface IGalleryService
    {
        void Save(IStreamer renderer, GalleryItemMetadata data);
        string GetDataFilename(string id);
        string GetItemFilename(GalleryItemMetadata data);
    }
}
