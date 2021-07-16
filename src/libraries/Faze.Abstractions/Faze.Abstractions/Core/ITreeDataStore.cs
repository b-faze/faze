namespace Faze.Abstractions.Core
{
    public interface ITreeDataStore<TId, TTreeValue>
    {
        void Save(Tree<TTreeValue> tree, TId id);
    }
}
