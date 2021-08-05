using Faze.Core.TreeLinq;
using System;

namespace Faze.Core.Data
{
    public class DynamicSquareTreeOptions<T> : DynamicTreeOptions<T>
    {
        public DynamicSquareTreeOptions(int size, int depth, Func<TreeMapInfo, T> valueFn) : base(size * size, depth, valueFn)
        {
        }
    }
}
