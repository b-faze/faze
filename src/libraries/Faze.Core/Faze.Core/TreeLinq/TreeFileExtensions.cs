using System;
using System.IO;
using Faze.Abstractions.Core;

namespace Faze.Core.TreeLinq
{
    public static class TreeFileExtensions
    {
        public static Tree<T> Load<T>(string id, ITreeDataProvider<T> treeDataProvider)
        {
            return treeDataProvider.Load(id);
        }

        public static void Save<T>(Tree<T> tree, string id, ITreeDataProvider<T> treeDataProvider)
        {
            treeDataProvider.Save(tree, id);
        }

    }
}
