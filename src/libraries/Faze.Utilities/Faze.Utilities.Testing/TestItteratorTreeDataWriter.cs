using Faze.Abstractions.Core;
using System;
using Faze.Core.TreeLinq;

namespace Faze.Utilities.Testing
{
    public class TestItteratorTreeDataWriter : ITreeDataWriter<string, object>
    {
        public void Save(Tree<object> tree, string id)
        {
            var nodes = tree.SelectDepthFirst();

            foreach (var node in nodes)
            {
                // do nothing
            }
        }
    }
}
