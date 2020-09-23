using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.TreeLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Faze.Rendering.Painters
{
    public class DepthPainter : ITreePainter
    {
        private readonly IColorInterpolator colorInterpolator;

        public DepthPainter() 
        {
            this.colorInterpolator = new GreyscaleInterpolator();
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

    public interface ITreePainter
    {
        PaintedTree Paint<T>(Tree<T> tree);
    }

    public interface IColorInterpolator 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">Value between 0 and 1</param>
        /// <returns>Color</returns>
        Color GetColor(double d);
    }
}
