using Faze.Abstractions.Core;
using Faze.Core.Data;
using Faze.Core.IO;
using System.IO;
using System.Reflection;

namespace Faze.Utilities.Testing
{
    public class TestEmbededResourceTreeDataReader<T> : ITreeDataReader<string, T>
    {
        private readonly ITreeSerialiser<T> treeSerialiser;
        private readonly Assembly assembly;
        private readonly string basePath;

        public TestEmbededResourceTreeDataReader(Assembly assembly, string basePath, IValueSerialiser<T> valueSerialiser)
        {
            this.treeSerialiser = new JsonTreeSerialiser<T>(valueSerialiser);
            this.assembly = assembly;
            this.basePath = basePath;
        }

        public Tree<T> Load(string id)
        {
            var resourceName = $"{basePath}.{id}";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return treeSerialiser.Deserialize(reader);
            }
        }
    }
}
