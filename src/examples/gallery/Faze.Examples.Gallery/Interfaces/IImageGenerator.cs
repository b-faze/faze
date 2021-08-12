using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Interfaces
{
    public interface IGalleryItemProvider
    {
        IEnumerable<GalleryItemMetadata> GetMetaData();
    }
}
