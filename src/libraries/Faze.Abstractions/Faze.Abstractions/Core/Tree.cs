using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            if (obj is Tree<TValue> tree)
            {
                if (Value != null && !Value.Equals(tree.Value))
                    return false;

                var childCount = Children?.Count();
                if (Children?.Count() != tree.Children?.Count())
                    return false;

                if (childCount == null)
                    return true;

                for (var i = 0; i < childCount.Value; i++) 
                {
                    var child = Children.ElementAt(i);
                    var otherChild = tree.Children.ElementAt(i);
                    if (!child.Equals(otherChild))
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}
