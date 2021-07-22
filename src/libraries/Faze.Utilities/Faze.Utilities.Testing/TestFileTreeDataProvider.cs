using Faze.Abstractions.Core;
using Faze.Core.Data;
using Faze.Core.IO;
using Faze.Core.Serialisers;
using Faze.Core.TreeLinq;
using System.IO;

namespace Faze.Utilities.Testing
{
    public class TestFileTreeDataProvider<T> : IFileTreeDataProvider<T>
    {
        private readonly FileTreeDataProvider<T> treeDataProvider;
        private readonly string basePath;

        public TestFileTreeDataProvider(string basePath, IValueSerialiser<T> valueSerialiser)
        {
            this.treeDataProvider = new FileTreeDataProvider<T>(new JsonTreeSerialiser<T>(valueSerialiser));
            this.basePath = basePath;
        }

        public Tree<T> Load(string id)
        {
            var filename = Path.Combine(basePath, id);
            return treeDataProvider.Load(filename);
        }

        public void Save(Tree<T> tree, string id)
        {
            var filename = Path.Combine(basePath, id);
            treeDataProvider.Save(tree, filename);
        }
    }
}
