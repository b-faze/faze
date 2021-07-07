using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeLinq;
using System;
using System.Drawing;
using System.Linq;

namespace Faze.Rendering.TreePainters
{
    public class DepthPainter : ITreePainter
    {
        private readonly IColorInterpolator colorInterpolator;

        public DepthPainter() 
        {
            this.colorInterpolator = new GreyscaleInterpolator();
        }

        public DepthPainter(IColorInterpolator colorInterpolator)
        {
            if (colorInterpolator == null)
                throw new NullReferenceException(nameof(colorInterpolator));

            this.colorInterpolator = colorInterpolator;
        }

        public Tree<Color> Paint<T>(Tree<T> tree)
        {
            var depthTree = tree
                .MapValue((v, info) => info.Depth);

            var maxDepth = depthTree
                .GetLeaves()
                .Max(x => x.Value);

            var colorTree = depthTree
                .MapValue(x => (double)x / maxDepth)
                .MapValue(colorInterpolator);

            return new Tree<Color>(colorTree.Value, colorTree.Children);
        }
    }
}
