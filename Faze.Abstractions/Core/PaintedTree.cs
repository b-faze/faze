using Faze.Abstractions.Rendering;
using System.Collections.Generic;
using System.Drawing;

namespace Faze.Abstractions.Core
{
    public class PaintedTree : IPaintedTree
    {
        public Color Value { get; }
        public IEnumerable<IPaintedTree> Children { get; }
    }
}
