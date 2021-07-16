using Faze.Abstractions.Core;

namespace Faze.Core
{
    public class DynamicTreeDataProvider<T> : ITreeDataProvider<DynamicSquareTreeOptions<T>, T>
    {
        public Tree<T> Load(DynamicSquareTreeOptions<T> options)
        {
            return options.CreateTree();
        }
    }
}
