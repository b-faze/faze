using System.Collections.Generic;

namespace Faze.Abstractions.GameResults
{
    public interface IResultAggregate<TAggregate>
    {
        TAggregate Value { get; }

        //void Add(TBase result);
        //void AddRange(IEnumerable<TBase> results);

        void Add(TAggregate result);
    }
}