using Faze.Abstractions.Core;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.TreeLinq
{
    /// <summary>
    /// Represents additional information for a tree node
    /// </summary>
    public class TreeMapInfo
    {
        private TreeMapInfo(TreeMapInfo parent, int depth, int childIndex) 
        {
            Parent = parent;
            Depth = depth;
            ChildIndex = childIndex;
        }

        public static TreeMapInfo Root()
        {
            return new TreeMapInfo(null, 0, 0);
        }

        public TreeMapInfo Parent { get; }
        public int Depth { get; }
        public int ChildIndex { get; }

        public IEnumerable<int> GetPath()
        {
            if (Parent == null)
                return new[] { ChildIndex };

            return Parent.GetPath().Concat(new[] { ChildIndex });
        }

        public TreeMapInfo Child(int childIndex)
        {
            return new TreeMapInfo(this, Depth + 1, childIndex);
        }
    }
}
