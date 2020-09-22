using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.Core
{
    public interface ITree<TValue>
    {
        TValue Value { get; }
        IEnumerable<ITree<TValue>> Children { get; }
    }
}
