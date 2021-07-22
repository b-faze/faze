using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Faze.Core.TreeLinq;
using System;
using System.Drawing;
using System.Linq;

namespace Faze.Rendering.TreePainters
{
    /// <summary>
    /// Paints each node in the tree according it its depth.
    /// The depth is normalised, so a max depth will have a value of 1 when it is passed to the color interpolator
    /// </summary>
    public class DepthTreePainter : ITreePainter
    {
        private readonly IColorInterpolator colorInterpolator;

        public DepthTreePainter() 
        {
            this.colorInterpolator = new GreyscaleInterpolator();
        }

        public DepthTreePainter(IColorInterpolator colorInterpolator)
        {
            if (colorInterpolator == null)
                throw new NullReferenceException(nameof(colorInterpolator));

            this.colorInterpolator = colorInterpolator;
        }

        public Tree<Color> Paint<T>(Tree<T> tree)
        {
            var depthTree = tree
                .MapValue((_, info) => info.Depth);

            var maxDepth = depthTree
                .GetLeaves()
                .Max(x => x.Value);

            var colorTree = depthTree
                .MapValue(x => (double)x / maxDepth)
                .MapValue(colorInterpolator);

            return colorTree;
        }
    }
}
