using System.IO;
using System.IO.Compression;
using Faze.Abstractions.Core;

namespace Faze.Core.Data
{
    public class FileTreeDataProviderConfig
    {
        public bool UseCompression { get; set; }
    }

    public class FileTreeDataProvider<T> : IFileTreeDataProvider<T>
    {
        private readonly ITreeSerialiser<T> treeSerialiser;
        private readonly FileTreeDataProviderConfig config;

        public FileTreeDataProvider(ITreeSerialiser<T> treeSerialiser, FileTreeDataProviderConfig config = null)
        {
            this.treeSerialiser = treeSerialiser;
            this.config = config ?? new FileTreeDataProviderConfig();
        }

        public virtual Tree<T> Load(string filename)
        {
            using (TextReader textReader = new StreamReader(OpenRead(filename)))
            {
                return treeSerialiser.Deserialize(textReader);
            }
        }

        public virtual void Save(Tree<T> tree, string filename, IProgressTracker progress)
        {
            using (Stream fileStream = OpenWrite(filename))
            using (TextWriter textWriter = new StreamWriter(fileStream))
            {
                treeSerialiser.Serialize(textWriter, tree, progress);
            }
        }

        private Stream OpenRead(string file)
        {
            var fileStream = File.OpenRead(file);
            if (config.UseCompression)
                return new GZipStream(fileStream, CompressionMode.Decompress);

            return fileStream;
        }

        private Stream OpenWrite(string file)
        {
            var fileStream = File.OpenWrite(file);
            if (config.UseCompression)
                return new GZipStream(fileStream, CompressionMode.Compress);

            return fileStream;
        }
    }
}
