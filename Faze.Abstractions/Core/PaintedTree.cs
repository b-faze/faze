using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Abstractions.Core
{
    public class PaintedTree : IPaintedTree
    {
        public Color Value { get; }
        public IEnumerable<IPaintedTree> Children { get; }
    }
}
