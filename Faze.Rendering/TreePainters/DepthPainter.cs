using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeLinq;
using System;
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

        public PaintedTree Paint<T>(Tree<T> tree)
        {
            var depthTree = tree
                .Map((v, info) => info.Depth);

            var maxDepth = depthTree
                .GetLeaves()
                .Max(x => x.Value);

            var colorTree = depthTree
                .Map(x => (double)x / maxDepth)
                .Map(colorInterpolator);

            return new PaintedTree(colorTree.Value, colorTree.Children);
        }
    }
}
