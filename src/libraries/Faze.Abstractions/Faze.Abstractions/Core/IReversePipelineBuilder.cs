using System;

namespace Faze.Abstractions.Core
{
    public interface IReversePipelineBuilder
    {
        IReversePipelineBuilder<TNext> Require<TNext>(Action<TNext> fn);
    }

    public interface IReversePipelineBuilder<T>
    {
        IReversePipelineBuilder<TNext> Require<TNext>(Func<TNext, T> fn);
        IPipeline<T> Build();
    }
}
