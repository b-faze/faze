using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System.Linq;

namespace Faze.Utilities.Testing
{
    public class TestRunCountTreeMapper : ITreeMapper<object, object>
    {
        public int NodeRunCount { get; private set; }
        public int TotalNodes { get; private set; }
        public int RunCount => NodeRunCount / TotalNodes;

        public Tree<object> Map(Tree<object> tree, IProgressTracker progress)
        {
            TotalNodes = tree.SelectDepthFirst().Count();

            return tree.MapValue(Map);
        }

        private object Map(object obj)
        {
            NodeRunCount++;

            return obj;
        }
    }
}
