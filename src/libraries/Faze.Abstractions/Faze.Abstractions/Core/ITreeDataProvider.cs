namespace Faze.Abstractions.Core
{
    public interface ITreeDataProvider<T>
    {
        Tree<T> Load(string id);
        void Save(Tree<T> tree, string id);
    }
}
