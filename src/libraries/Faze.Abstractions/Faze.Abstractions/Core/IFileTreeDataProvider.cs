namespace Faze.Abstractions.Core
{
    public interface IFileTreeDataProvider<T> : ITreeDataProvider<string, T>, ITreeDataStore<string, T>
    {

    }
}
