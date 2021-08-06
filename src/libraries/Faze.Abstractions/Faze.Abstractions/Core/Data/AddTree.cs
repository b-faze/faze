using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Abstractions.Core
{
    public class AddTreeConfig<TValue>
    {
        public AddTreeConfig()
        {
            AddValueFn = (existing, value) => value;
        }

        public Func<TValue, TValue, TValue> AddValueFn { get; set; }
        public bool AddWhileTraversing { get; set; }
        public bool PathInvariant { get; set; }
    }
    public class AddTree<TValue> : Tree<TValue>
    {
        private readonly int index;
        private AddTreeConfig<TValue> config;
        private readonly LinkedList<AddTree<TValue>> children;

        public AddTree(AddTreeConfig<TValue> config) : this(0, default(TValue), config)
        {
        }

        public AddTree(int index, AddTreeConfig<TValue> config) : this(index, default(TValue), config)
        {
        }

        public AddTree(int index, TValue value, AddTreeConfig<TValue> config) : base(value)
        {
            this.index = index;
            this.config = config;
            this.children = new LinkedList<AddTree<TValue>>();
        }

        public override IEnumerable<Tree<TValue>> Children => children;

        public void Add(int[] path, TValue value)
        {
            if (config.AddWhileTraversing || path.Length == 0)
            {
                Value = config.AddValueFn(Value, value);
            }

            if (path.Length == 0)
            {
                return;
            }

            if (config.PathInvariant)
            {
                for (var i = 0; i < path.Length; i++)
                {
                    var index = path[i];
                    var newPath = path.Take(i).Concat(path.Skip(i + 1)).ToArray();
                    GetOrAdd(index).Add(newPath, value);
                }

                return;
            }

            var child = GetOrAdd(path[0]);

            child.Add(path.Skip(1).ToArray(), value);
        }

        public AddTree<TValue> GetOrAdd(int index)
        {
            var node = children.First;
            var newChild = new AddTree<TValue>(index, config);

            if (node == null)
            {
                children.AddFirst(newChild);
                return newChild;
            }

            while (node != null && node.Value.index < index)
                node = node.Next;

            if (node != null && node.Value.index == index)
                return node.Value;
            
            if (node != null)
            {
                children.AddBefore(node, newChild);
            }
            else
            {
                children.AddLast(newChild);
            }

            return newChild;
        }
    }
}
