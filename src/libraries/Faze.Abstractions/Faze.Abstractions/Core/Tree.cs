using System;
using System.Collections.Generic;
using System.Drawing;

namespace Faze.Abstractions.Core
{
    public class Tree<TValue>
    {
        public Tree(TValue value)
        {
            Value = value;
        }

        public Tree(TValue value, IEnumerable<Tree<TValue>> children)
        {
            Value = value;
            Children = children;
        }

        public TValue Value { get; }
        public IEnumerable<Tree<TValue>> Children { get; }
    }
}
