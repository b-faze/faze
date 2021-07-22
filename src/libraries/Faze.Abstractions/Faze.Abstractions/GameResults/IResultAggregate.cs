using System.Collections.Generic;

namespace Faze.Abstractions.GameResults
{
    /// <summary>
    /// Represents a type which can aggregate results of type <typeparamref name="TAggregate"/>
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    public interface IResultAggregate<TAggregate>
    {
        TAggregate Value { get; }

        void Add(TAggregate result);
    }
}