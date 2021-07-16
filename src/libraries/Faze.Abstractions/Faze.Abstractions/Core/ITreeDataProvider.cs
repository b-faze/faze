namespace Faze.Abstractions.Core
{
    public interface ITreeDataProvider<TId, TTreeValue>
    {
        Tree<TTreeValue> Load(TId id);
    }
}
