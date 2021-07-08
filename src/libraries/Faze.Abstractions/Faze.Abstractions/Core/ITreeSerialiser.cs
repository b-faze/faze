using System.IO;

namespace Faze.Abstractions.Core
{
    public interface ITreeSerialiser<T>
    {
        void Serialize(TextWriter textWriter, Tree<T> tree);
        Tree<T> Deserialize(TextReader textReader);
    }

    public interface IValueSerialiser<T>
    {
        string Serialize(T value);
        T Deserialize(string valueString);
    }
}
