using Faze.Abstractions.GameMoves;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using System;

namespace Faze.Rendering.TreeRenderers
{
    public static class SquareTreeRendererExtensions
    {
        public static IViewport GetFinalViewport(int[] path, int dimension, float borderProportion)
        {
            float x = 0;
            float y = 0;

            int depth = 0;
            float scale = 1;
            foreach (GridMove moveIndex in path)
            {
                // border compensation
                var borderOffset = (1 / (float)Math.Pow(dimension, depth)) * borderProportion;
                x += borderOffset;
                y += borderOffset;

                depth++;

                float dx = moveIndex.GetX(dimension);
                float dy = moveIndex.GetY(dimension);

                scale = (float)Math.Pow(dimension, depth);
                x += dx / scale;
                y += dy / scale;
            }

            return new Viewport(x, y, 1 / scale);
        }
    }
}
