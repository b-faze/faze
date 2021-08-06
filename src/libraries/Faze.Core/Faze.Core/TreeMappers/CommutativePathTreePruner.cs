using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.TreeMappers
{
    /// <summary>
    /// Prunes nodes with the same path
    /// The path order to get to a node is assumed to not matter, 
    /// therefore nodes reached with a different order are considered duplicate nodes and will be pruned.
    /// </summary>
    public class CommutativePathTreePruner : ITreeStructureMapper
    {
        public Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress = null)
        {
            progress = progress ?? NullProgressTracker.Instance;

            return MapHelper(tree, TreeMapInfo.Root(), new HashSet<string>(), progress);
        }

        public static Tree<T> MapHelper<T>(Tree<T> tree, TreeMapInfo info, HashSet<string> visitedPaths, IProgressTracker progress)
        {
            if (info.Depth <= 3)
                progress.SetMessage(string.Join(",", info.GetPath()));

            if (tree == null)
                return null;

            var path = info.GetPath().ToArray();
            var pathOriginalHash = info.GetPathHash();
            var pathHash = info.GetPathHash(ordered: true);


            if (visitedPaths.Contains(pathHash))
            {
                if (pathOriginalHash != pathHash)
                {
                    return null;
                }
            } 
            else if (path.Length > 1)
            {
                visitedPaths.Add(pathHash);
            }

            var children = tree.Children?.Select((child, i) => MapHelper(child, info.Child(i), visitedPaths, progress));
            var newTree = new Tree<T>(tree.Value, children);

            return newTree;
        }
    }
}
