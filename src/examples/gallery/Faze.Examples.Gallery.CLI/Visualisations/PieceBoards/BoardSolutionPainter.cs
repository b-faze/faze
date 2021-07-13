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
    public class BoardSolutionPainter : ITreePainter<IGameState<GridMove, SingleScoreResult?>>
    {
        private readonly IColorInterpolator colorInterpolator;

        public BoardSolutionPainter()
        {
            this.colorInterpolator = new GreyscaleInterpolator();
        }

        public BoardSolutionPainter(IColorInterpolator colorInterpolator)
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
    }
}
