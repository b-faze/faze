using Faze.Abstractions.Core;
using System.Collections.Generic;

namespace Faze.Abstractions.Rendering
{
    public interface IIterater<TIn, TOut>
    {
        IEnumerable<TOut> GetEnumerable(TIn input, IProgressTracker progress = null);
    }
}
