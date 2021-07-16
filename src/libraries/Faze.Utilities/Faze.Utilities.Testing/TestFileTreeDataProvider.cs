using Faze.Abstractions.Core;
using Faze.Core.IO;
using Faze.Core.Serialisers;
using Faze.Core.TreeLinq;
using System;
using System.IO;

namespace Faze.Utilities.Testing
{
    public class TestFileTreeDataProvider : IFileTreeDataProvider<int?>
    {
        private readonly FileTreeDataProvider<int?> treeDataProvider;
        private readonly string basePath;

        public TestFileTreeDataProvider(string basePath)
        {
            this.treeDataProvider = new FileTreeDataProvider<int?>(new JsonTreeSerialiser<int?>(new NullableIntSerialiser()));
            this.basePath = basePath;
        }

        public Tree<int?> Load(string id)
        {
            var filename = Path.Combine(basePath, id);
            return treeDataProvider.Load(filename);
        }

        public void Save(Tree<int?> tree, string id)
        {
            var filename = Path.Combine(basePath, id);
            treeDataProvider.Save(tree, filename);
        }
    }
}
