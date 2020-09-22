using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Rendering
{
    internal static class Utilities
    {
        public static (int x, int y, int width, int height) Flatten(int[] path, int dimension, int width, int height)
        {
            var x = 0;
            var y = 0;

            for (var i = 0; i < path.Length; i++)
            {
                var move = path[i];
                var subFactor = (int)Math.Pow(dimension, i + 1);
                var (dx, dy) = To2D(move, dimension);

                x += width / subFactor * dx;
                y += height / subFactor * dy;
            }

            var factor = (int)Math.Pow(dimension, path.Length);
            return (x, y, width / factor, height / factor);
        }

        public static (int x, int y) To2D(int index, int width)
        {
            return (index % width, index / width);
        }

        public static int To1D(int x, int y, int width)
        {
            return x + y * width;
        }

        public static IEnumerable<(int x, int y)> GetPixels(int x, int y, int width, int height)
        {
            for (var i = x; i < x + width; i++)
            {
                for (var j = y; j < y + height; j++)
                {
                    yield return (i, j);
                }
            }
        }
    }

}
