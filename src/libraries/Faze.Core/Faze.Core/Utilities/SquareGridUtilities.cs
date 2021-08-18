using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Core.Utilities
{
    public static class SquareGridUtilities
    {
        public static IEnumerable<(int x, int y)> GetDiagonals((int x, int y) pos, int dimension)
        {
            (int dx, int dy) direction = (1, 1);

            var minPos = Math.Min(pos.x, pos.y);
            (int x, int y) start = (pos.x - minPos, pos.y - minPos);
            var limit = dimension - Math.Max(start.x, start.y);
            for (var i = 0; i < limit; i++)
            {
                yield return (start.x + direction.dx * i, start.y + direction.dy * i);
            }

            direction = (1, -1);
            start = pos.x + pos.y < dimension - 1
                ? (0, pos.y + pos.x)
                : (pos.x - (dimension - 1 - pos.y), dimension - 1);

            limit = Math.Min(start.y + 1, dimension - start.x);
            for (var i = 0; i < limit; i++)
            {
                yield return (start.x + direction.dx * i, start.y + direction.dy * i);
            }
        }
    }
}
