using System;
using System.IO;
using Faze.Abstractions.Core;

namespace Faze.Core.TreeLinq
{
    public static class TreeFileExtensions
    {
        public static Tree<T> Load<T>(string filename, ITreeSerialiser<T> serializer)
        {
            using (TextReader textReader = new StreamReader(File.OpenRead(filename)))
            {
                return serializer.Deserialize(textReader);
            }
        }

        public static void Save<T>(Tree<T> tree, string filename, ITreeSerialiser<T> serializer)
        {
            using (TextWriter textWriter = new StreamWriter(File.OpenWrite(filename)))
            {
                serializer.Serialize(textWriter, tree);
            }
        }

    }
}
