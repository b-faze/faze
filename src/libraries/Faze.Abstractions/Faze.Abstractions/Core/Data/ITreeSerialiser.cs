using System.IO;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Reads and writes a Tree to a stream as text
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITreeSerialiser<T>
    {
        void Serialize(TextWriter textWriter, Tree<T> tree);
        Tree<T> Deserialize(TextReader textReader);
    }
}
