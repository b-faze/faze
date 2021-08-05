using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.TreeMappers
{
    public class MoveOrderInvariantTreeMapper2 : ITreeStructureMapper
    {
        public Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress)
        {
            return PathOrderInveriantHelper(tree, TreeMapInfo.Root(), new HashSet<string>());
        }

        public static Tree<T> PathOrderInveriantHelper<T>(Tree<T> tree, TreeMapInfo info, HashSet<string> visitedPaths)
        {
            if (tree == null)
                return null;

            var path = info.GetPath().ToArray();
            var pathOriginalHash = string.Join(",", path);
            var pathHash = string.Join(",", path.OrderBy(x => x));


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

            var children = tree.Children?.Select((child, i) => PathOrderInveriantHelper(child, info.Child(i), visitedPaths));
            var newTree = new Tree<T>(tree.Value, children);

            return newTree;
        }
    }
}
