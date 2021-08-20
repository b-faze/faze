using System.Collections.Generic;

namespace Faze.Abstractions.Core
{
    public interface ITreeAdapter<T>
    {
        IEnumerable<T> GetChildren(T state);
    }
}
