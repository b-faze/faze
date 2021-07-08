using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.TreeLinq;
using System;
using System.Drawing;

namespace Faze.Core.Pipelines
{
    public static class PipelineExtensions
    {
        public static IReversePipelineBuilder<IPaintedTreeRenderer> File(this IReversePipelineBuilder builder, string filename) 
        {
            return builder.Require<IPaintedTreeRenderer>(renderer => renderer.SaveToFile(filename));
        }

        public static IReversePipelineBuilder<Tree<Color>> Render(this IReversePipelineBuilder<IPaintedTreeRenderer> builder, IPaintedTreeRenderer renderer)
        {
            return builder.Require<Tree<Color>>(tree =>
            {
                renderer.Draw(tree);
                return renderer;
            });
        }

        public static IReversePipelineBuilder<Tree<T>> Paint<T>(this IReversePipelineBuilder<Tree<Color>> builder, ITreePainter painter)
        {
            return builder.Require<Tree<T>>(tree =>
            {
                return painter.Paint(tree);
            });
        }

        public static IReversePipelineBuilder<Tree<double>> Paint(this IReversePipelineBuilder<Tree<Color>> builder, IColorInterpolator colorInterpolator)
        {
            return builder.Require<Tree<double>>(tree =>
            {
                return tree.MapValue(colorInterpolator);
            });
        }

        public static IReversePipelineBuilder<Tree<TIn>> MapTree<TOut, TIn>(this IReversePipelineBuilder<Tree<TOut>> builder, Func<Tree<TIn>, Tree<TOut>> fn)
        {
            return builder.Require(fn);
        }

        public static IReversePipelineBuilder<Tree<TIn>> Map<TOut, TIn>(this IReversePipelineBuilder<Tree<TOut>> builder, ITreeMapper<TIn, TOut> mapper)
        {
            return builder.Require<Tree<TIn>>(tree =>
            {
                return mapper.Map(tree);
            });
        }

        public static IReversePipelineBuilder<Tree<TIn>> MapValue<TOut, TIn>(this IReversePipelineBuilder<Tree<TOut>> builder, Func<TIn, TOut> fn)
        {
            return builder.Require<Tree<TIn>>(tree =>
            {
                return tree.MapValue(fn);
            });
        }

        public static IReversePipelineBuilder<IGameState<TMove, TResult>> GameTree<TMove, TResult>(this IReversePipelineBuilder<Tree<IGameState<TMove, TResult>>> builder, IGameStateTreeAdapter<TMove> adapter)
        {
            return builder.Require<IGameState<TMove, TResult>>(state =>
            {
                return state.ToStateTree(adapter);
            });
        }
    }
}
