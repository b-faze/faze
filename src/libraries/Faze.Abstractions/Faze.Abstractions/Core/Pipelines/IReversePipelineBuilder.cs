using System;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Used to build up pipeline steps in reverse order
    /// </summary>
    public interface IReversePipelineBuilder
    {
        IReversePipelineBuilder<T, T> Require<T>();
        IReversePipelineBuilder<TNext> Require<TNext>(Action<TNext> fn);
        IReversePipelineBuilder<TNext> Require<TNext>(Action<TNext, IProgressTracker> fn);
    }

    /// <summary>
    /// Used to build up pipeline steps in reverse order,
    /// requiring an input of type <typeparamref name="T"/>
    /// </summary>
    public interface IReversePipelineBuilder<T>
    {
        IReversePipelineBuilder<TNext> Require<TNext>(Func<TNext, T> fn);
        IReversePipelineBuilder<TNext> Require<TNext>(Func<TNext, IProgressTracker, T> fn);

        /// <summary>
        /// Builds a IPipeline that requires an input of type <typeparamref name="T"/>
        /// </summary>
        /// <returns></returns>
        IPipeline<T> Build();

        /// <summary>
        /// Builds a IPipeline that generates its own initial input of type <typeparamref name="T"/>
        /// </summary>
        /// <param name="fn">Factory to generate initial input value</param>
        /// <returns></returns>
        IPipeline Build(Func<T> fn);
    }

    public interface IReversePipelineBuilder<TOut, TIn>
    {
        IReversePipelineBuilder<TOut, TNext> Require<TNext>(Func<TNext, TIn> fn);
        IReversePipelineBuilder<TOut, TNext> Require<TNext>(Func<TNext, IProgressTracker, TIn> fn);

        /// <summary>
        /// Builds a IPipeline that requires an input of type <typeparamref name="T"/>
        /// </summary>
        /// <returns></returns>
        IPipeline<TIn, TOut> Build();

        /// <summary>
        /// Builds a IPipeline that generates its own initial input of type <typeparamref name="T"/>
        /// </summary>
        /// <param name="fn">Factory to generate initial input value</param>
        /// <returns></returns>
        IPipeline<TOut> Build(Func<TIn> fn);
    }
}
