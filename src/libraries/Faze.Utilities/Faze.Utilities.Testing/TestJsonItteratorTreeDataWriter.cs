using Faze.Abstractions.Core;
using Faze.Core.IO;
using System.IO;

namespace Faze.Utilities.Testing
{
    public class TestJsonItteratorTreeDataWriter : ITreeDataWriter<string, object>
    {
        private readonly ITreeSerialiser<object> treeSerialiser;

        public TestJsonItteratorTreeDataWriter()
        {
            this.treeSerialiser = new JsonTreeSerialiser<object>(new TestNullObjectSerialiser());
        }

        public void Save(Tree<object> tree, string id)
        {
            using (Stream ms = new MemoryStream())
            using (TextWriter textWriter = new StreamWriter(ms))
            {
                treeSerialiser.Serialize(textWriter, tree);
            }
        }
    }
}
