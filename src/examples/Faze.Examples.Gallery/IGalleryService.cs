using Faze.Abstractions.Rendering;
using System.IO;

namespace Faze.Examples.Gallery
{
    public interface IGalleryService
    {
        void Save(IPaintedTreeRenderer renderer, GalleryItemMetadata data);
        string GetDataFilename(string id);
    }
}
