using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Core.TreeMappers
{
    public class MoveOrderInvariantTreeMapper : ITreeStructureMapper
    {
        public Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress)
        {
            return PathOrderInveriantHelper(tree, TreeMapInfo.Root(), new Dictionary<string, Tree<T>>(), progress);
        }

        public static Tree<T> PathOrderInveriantHelper<T>(Tree<T> tree, TreeMapInfo info, Dictionary<string, Tree<T>> lookup, IProgressTracker progress)
        {
            if (info.Depth <= 3)
                progress.SetMessage(string.Join(",", info.GetPath()));

            var path = info.GetPath().ToArray();
            var pathOriginalHash = string.Join(",", path);
            var pathHash = string.Join(",", path.OrderBy(x => x));

            if (lookup.ContainsKey(pathHash))
                return lookup[pathHash];

            if (tree == null)
                return null;

            var children = tree.Children?.Select((child, i) => PathOrderInveriantHelper(child, info.Child(i), lookup, progress)).ToList();
            var newTree = new Tree<T>(tree.Value, children);

            if (path.Length > 1)
            {
                //tree = tree.Evaluate();
                lookup.Add(pathHash, newTree);
            }

            return newTree;
        }
    }
}
