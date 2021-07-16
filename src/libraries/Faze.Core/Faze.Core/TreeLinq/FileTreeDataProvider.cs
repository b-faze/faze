using System.IO;
using Faze.Abstractions.Core;

namespace Faze.Core.TreeLinq
{
    public class FileTreeDataProvider<T> : IFileTreeDataProvider<T>
    {
        private readonly ITreeSerialiser<T> treeSerialiser;

        public FileTreeDataProvider(ITreeSerialiser<T> treeSerialiser)
        {
            this.treeSerialiser = treeSerialiser;
        }

        public virtual Tree<T> Load(string filename)
        {
            using (TextReader textReader = new StreamReader(File.OpenRead(filename)))
            {
                return treeSerialiser.Deserialize(textReader);
            }
        }

        public virtual void Save(Tree<T> tree, string filename)
        {
            using (TextWriter textWriter = new StreamWriter(File.OpenWrite(filename)))
            {
                treeSerialiser.Serialize(textWriter, tree);
            }
        }
    }
}
