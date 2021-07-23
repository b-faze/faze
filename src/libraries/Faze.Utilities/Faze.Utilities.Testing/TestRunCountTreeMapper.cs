using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Linq;

namespace Faze.Utilities.Testing
{
    public class TestRunCountTreeMapper : ITreeStructureMapper
    {
        public int NodeRunCount { get; private set; }
        public int TotalNodes { get; private set; }
        public int RunCount => NodeRunCount / TotalNodes;

        public Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress)
        {
            TotalNodes = tree.SelectDepthFirst().Count();

            return tree.MapValue(Map);
        }

        private T Map<T>(T obj)
        {
            NodeRunCount++;

            return obj;
        }
    }
}
