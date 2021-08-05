using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Represents a tree data structure which holds a value of type <typeparamref name="TValue"/> at each node
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
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

        public TValue Value { get; protected set; }
        public virtual IEnumerable<Tree<TValue>> Children { get; }

        public override bool Equals(object obj)
        {
            if (obj is Tree<TValue> tree)
            {
                if (Value != null && !Value.Equals(tree.Value))
                    return false;

                var childCount = Children?.Count();
                if ((Children?.Where(x => x != null).Count() ?? 0) != (tree.Children?.Where(x => x != null).Count() ?? 0))
                    return false;

                if (childCount == null)
                    return true;

                for (var i = 0; i < childCount.Value; i++) 
                {
                    var child = Children.ElementAt(i);
                    var otherChild = tree.Children.ElementAt(i);
                    if (child == null && otherChild == null)
                        continue;

                    if (child == null && otherChild != null)
                        return false;

                    if (child != null && !child.Equals(otherChild))
                        return false;
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
