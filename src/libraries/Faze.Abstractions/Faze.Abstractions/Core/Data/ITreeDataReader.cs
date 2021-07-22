namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Loads a Tree given an id of type <typeparamref name="TId"/>
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TTreeValue"></typeparam>
    public interface ITreeDataReader<TId, TTreeValue>
    {
        Tree<TTreeValue> Load(TId id);
    }
}
