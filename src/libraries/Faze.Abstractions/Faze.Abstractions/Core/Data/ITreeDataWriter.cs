namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Saves a Tree against given id of type <typeparamref name="TId"/>
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TTreeValue"></typeparam>
    public interface ITreeDataWriter<TId, TTreeValue>
    {
        void Save(Tree<TTreeValue> tree, TId id, IProgressTracker progress = null);
    }
}
