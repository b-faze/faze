using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.Drawing;

namespace Faze.Abstractions.Rendering
{
    public class PaintedTree : Tree<Color>
    {
        public PaintedTree(Color value) : base(value)
        {
        }

        public PaintedTree(Color value, IEnumerable<Tree<Color>> children) : base(value, children)
        {
        }
    }
}
