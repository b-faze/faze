using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Core.Streamers;
using Faze.Core.TreeLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Faze.Core.Extensions
{
    public static class ReversePipelineExtensions
    {
        public static IReversePipelineBuilder<Stream> StreamStreamer(this IReversePipelineBuilder<IStreamer> builder)
        {
            return builder.Require<Stream>(stream => new StreamStreamer(stream));
        }

        public static IReversePipelineBuilder<IEnumerable<Stream>> Merge(this IReversePipelineBuilder<Stream> builder)
        {
            return builder.Require<IEnumerable<Stream>>(streams => new ConcatenatedStream(streams));
        }

        public static IReversePipelineBuilder<IEnumerable<IStreamer>> Merge(this IReversePipelineBuilder<IStreamer> builder)
        {
            return builder.Require<IEnumerable<IStreamer>>(streamers => new EnumerableStreamer(streamers));
        }

        public static IReversePipelineBuilder<IStreamer> File(this IReversePipelineBuilder builder, string filename) 
        {
            return builder.Require<IStreamer>(renderer => renderer.SaveToFile(filename));
        }

        public static IReversePipelineBuilder<Tree<Color>> Render(this IReversePipelineBuilder<IStreamer> builder, IPaintedTreeRenderer renderer)
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

        public static IReversePipelineBuilder<Tree<T>> Paint<T>(this IReversePipelineBuilder<Tree<Color>> builder, ITreePainter<T> painter)
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

        public static IReversePipelineBuilder<IEnumerable<TIn>> Map<TOut, TIn>(this IReversePipelineBuilder<IEnumerable<TOut>> builder, Func<IReversePipelineBuilder<TOut>, IReversePipelineBuilder<TIn>> innerBuilderFactory)
        {
            var innerBuilder = (IReversePipelineBuilder<TOut, TIn>)innerBuilderFactory((IReversePipelineBuilder<TOut>)ReversePipelineBuilder.Create().Require<TOut>());
            var pipeline = innerBuilder.Build();
            return builder.Require<IEnumerable<TIn>>(items => items.Select(item => pipeline.Run(item)));
        }

        public static IReversePipelineBuilder<Tree<TIn>> Map<TOut, TIn>(this IReversePipelineBuilder<Tree<TOut>> builder, Func<Tree<TIn>, Tree<TOut>> fn)
        {
            return builder.Require(fn);
        }

        public static IReversePipelineBuilder<Tree<TIn>> Map<TOut, TIn>(this IReversePipelineBuilder<Tree<TOut>> builder, ITreeMapper<TIn, TOut> mapper)
        {
            return builder.Require<Tree<TIn>>((tree, progress) =>
            {
                return mapper.Map(tree, progress);
            });
        }

        public static IReversePipelineBuilder<Tree<T>> Map<T>(this IReversePipelineBuilder<Tree<T>> builder, ITreeStructureMapper mapper)
        {
            return builder.Require<Tree<T>>((tree, progress) =>
            {
                return mapper.Map(tree, progress);
            });
        }

        public static IReversePipelineBuilder<T> Iterate<T>(this IReversePipelineBuilder<IEnumerable<T>> builder, int n, Action fn)
        {
            return builder.Require<T>(input =>
            {
                return Enumerable.Range(0, n).Select(_ =>
                {
                    fn();
                    return input;
                });
            });
        }

        public static IReversePipelineBuilder<TIn> Iterate<TOut, TIn>(this IReversePipelineBuilder<IEnumerable<TOut>> builder, IIterater<TIn, TOut> iterater)
        {
            return builder.Require<TIn>((input, progress) =>
            {
                return iterater.GetEnumerable(input, progress);
            });
        }

        public static IReversePipelineBuilder<Tree<T>> Evaluate<T>(this IReversePipelineBuilder<Tree<T>> builder)
        {
            return builder.Require<Tree<T>>(tree =>
            {
                return tree.Evaluate();
            });
        }

        public static IReversePipelineBuilder<Tree<T>> LimitDepth<T>(this IReversePipelineBuilder<Tree<T>> builder, int maxDepth)
        {
            return builder.Require<Tree<T>>(tree =>
            {
                return tree.LimitDepth(maxDepth);
            });
        }

        public static IReversePipelineBuilder<IGameState<TMove, TResult>> GameTree<TMove, TResult>(this IReversePipelineBuilder<Tree<IGameState<TMove, TResult>>> builder, IGameStateTreeAdapter<TMove> adapter = null)
        {
            return builder.Require<IGameState<TMove, TResult>>(state =>
            {
                return adapter != null 
                    ? state.ToStateTree(adapter)
                    : state.ToStateTree();
            });
        }

        public static IReversePipelineBuilder<T> GameTree<T>(this IReversePipelineBuilder<Tree<T>> builder, ITreeAdapter<T> adapter)
        {
            return builder.Require<T>(state =>
            {
                return state.ToStateTree(adapter);
            });
        }

        public static IPipeline LoadTree<TId, T>(this IReversePipelineBuilder<Tree<T>> builder, TId id, ITreeDataReader<TId, T> treeDataProvider)
        {
            return builder.Build(() =>
            {
                return treeDataProvider.Load(id);
            });
        }

        public static IReversePipelineBuilder<Tree<T>> SaveTree<TId, T>(this IReversePipelineBuilder builder, TId id, ITreeDataWriter<TId,T> treeDataStore)
        {
            return builder.Require<Tree<T>>((tree, progress) =>
            {
                treeDataStore.Save(tree, id, progress);
            });
        }


    }
}
