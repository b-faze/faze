using Faze.Abstractions.Core;
using System.CodeDom.Compiler;

namespace Faze.Rendering.TreeLinq
{
    public class TreeMapInfo
    {
        public TreeMapInfo(int depth, int childIndex) 
        {
            Depth = depth;
            ChildIndex = childIndex;
        }

        public static TreeMapInfo Root()
        {
            return new TreeMapInfo(0, 0);
        }

        public int Depth { get; }
        public int ChildIndex { get; }
    }
}
