using Faze.Abstractions.Core;
using Faze.Core.Data;
using Faze.Examples.Gallery.Interfaces;

namespace Faze.Examples.Gallery.Services
{
    public class GalleryTreeDataProvider<T> : FileTreeDataProvider<T>
    {
        private readonly IGalleryService galleryService;

        public GalleryTreeDataProvider(IGalleryService galleryService, ITreeSerialiser<T> treeSerialiser) : base(treeSerialiser, new FileTreeDataProviderConfig
        {
            UseCompression = false
        })
        {
            this.galleryService = galleryService;
        }

        public override Tree<T> Load(string id)
        {
            return base.Load(galleryService.GetDataFilename(id));
        }

        public override void Save(Tree<T> tree, string id, IProgressTracker progress)
        {
            base.Save(tree, galleryService.GetDataFilename(id), progress);
        }
    }
}
