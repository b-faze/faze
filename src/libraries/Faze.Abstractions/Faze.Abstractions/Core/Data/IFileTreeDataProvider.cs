namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Provides read and write functionality using a string id
    /// The id is indended to be the path of the file
    /// </summary>
    /// <typeparam name="T">Tree value type</typeparam>
    public interface IFileTreeDataProvider<T> : ITreeDataReader<string, T>, ITreeDataWriter<string, T>
    {

    }
}
