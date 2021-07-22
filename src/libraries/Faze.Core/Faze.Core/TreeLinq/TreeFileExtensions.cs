using System;
using System.IO;
using Faze.Abstractions.Core;

namespace Faze.Core.TreeLinq
{
    public static class TreeFileExtensions
    {
        public static Tree<T> Load<TId, T>(TId id, ITreeDataReader<TId, T> treeDataProvider)
        {
            return treeDataProvider.Load(id);
        }

        public static void Save<TId, T>(Tree<T> tree, TId id, ITreeDataWriter<TId, T> treeDataStore)
        {
            treeDataStore.Save(tree, id);
        }

    }
}
