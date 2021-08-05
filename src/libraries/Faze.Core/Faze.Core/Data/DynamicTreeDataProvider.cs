using Faze.Abstractions.Core;

namespace Faze.Core.Data
{
    public class DynamicTreeDataProvider<T> : ITreeDataReader<DynamicTreeOptions<T>, T>
    {
        public Tree<T> Load(DynamicTreeOptions<T> options)
        {
            return options.CreateTree();
        }
    }
}
