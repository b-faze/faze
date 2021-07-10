using Faze.Abstractions.Core;
using Faze.Core.TreeLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Utilities
{
    public class GalleryTreeDataProvider<T> : FileTreeDataProvider<T>
    {
        private readonly IGalleryService galleryService;

        public GalleryTreeDataProvider(IGalleryService galleryService, ITreeSerialiser<T> treeSerialiser) : base(treeSerialiser)
        {
            this.galleryService = galleryService;
        }

        public override Tree<T> Load(string id)
        {
            return base.Load(galleryService.GetDataFilename(id));
        }

        public override void Save(Tree<T> tree, string id)
        {
            base.Save(tree, galleryService.GetDataFilename(id));
        }
    }
}
