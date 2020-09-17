using System.Collections.Generic;

namespace Faze.Abstractions
{
    public interface IResultAggregate<TResult>
    {
        void Add(TResult result);
        string Serialize();
        IResultAggregate<TResult> Deserialise(string v);
    }
}