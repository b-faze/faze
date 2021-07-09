using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using System;
using System.Drawing;
using Faze.Core.TreeLinq;
using System.Linq;

namespace Faze.Examples.Gallery.CLI.Visualisations.PieceBoards
{
    public class BoardPainter : ITreePainter<IGameState<GridMove, SingleScoreResult?>>
    {
        private readonly IColorInterpolator colorInterpolator;

        public BoardPainter()
        {
            this.colorInterpolator = new GreyscaleInterpolator();
        }

        public BoardPainter(IColorInterpolator colorInterpolator)
        {
            if (colorInterpolator == null)
                throw new NullReferenceException(nameof(colorInterpolator));

            this.colorInterpolator = colorInterpolator;
        }


        public Tree<Color> Paint(Tree<IGameState<GridMove, SingleScoreResult?>> tree)
        {
            var resultTree = tree.MapValue((x, info) =>
            {
                var result = x.GetResult();
                if (result != null)
                    return Math.Max(result.Value, 0);

                return info.Depth;
            });

            var maxValue = resultTree
                .GetLeaves()
                .Max(t => t.Value);

            var colorTree = resultTree
                .MapValue(x => (double)x / maxValue)
                .MapValue(colorInterpolator);

            return colorTree;
        }

        //public Tree<Color> PaintOld(Tree<IGameState<GridMove, SingleScoreResult?>> tree)
        //{
        //    var depthTree = tree
        //        .MapValue((_, info) => info.Depth);

        //    var maxDepth = depthTree
        //        .GetLeaves()
        //        .Max(x => x.Value);

        //    var colorTree = depthTree
        //        .MapValue(x => (double)x / maxDepth)
        //        .MapValue(colorInterpolator)
        //        .MapTreeWithNullNodes((t, info) =>
        //        {
        //            if (t != null)
        //                return t.Value;

        //            if (info.ChildIndex == info.Parent.ChildIndex)
        //                return Color.Transparent;

        //            return Color.Black;
        //        });

        //    return colorTree;
        //}
    }
}
