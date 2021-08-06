using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Core.TreeMappers
{
    /// <summary>
    /// Merges nodes with the same path
    /// The path order to get to a node is assumed to not matter, 
    /// therefore navigating via a different path order will send you to the same node.
    /// Can be used with the CummutativePathTreePruner to 'recover' the pruned sections
    /// </summary>
    public class CommutativePathTreeMerger : ITreeStructureMapper
    {
        public Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress = null)
        {
            progress = progress ?? NullProgressTracker.Instance;

            return MapHelper(tree, TreeMapInfo.Root(), new Dictionary<string, Tree<T>>(), progress);
        }

        public static Tree<T> MapHelper<T>(Tree<T> tree, TreeMapInfo info, Dictionary<string, Tree<T>> lookup, IProgressTracker progress)
        {
            if (info.Depth <= 3)
                progress.SetMessage(info.GetPathHash());

            var path = info.GetPath().ToArray();
            var pathOriginalHash = info.GetPathHash();
            var pathHash = info.GetPathHash(ordered: true);

            if (lookup.ContainsKey(pathHash))
                return lookup[pathHash];

            if (tree == null)
                return null;

            var children = tree.Children?.Select((child, i) => MapHelper(child, info.Child(i), lookup, progress)).ToList();
            var newTree = new Tree<T>(tree.Value, children);

            if (path.Length > 1)
            {
                lookup.Add(pathHash, newTree);
            }

            return newTree;
        }
    }
}
