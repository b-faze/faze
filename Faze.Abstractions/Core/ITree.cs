using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.Core
{
    public interface ITree<TTree, TValue> where TTree : ITree<TTree, TValue>
    {
        TValue Value { get; }
        IEnumerable<TTree> Children { get; }
    }
}
