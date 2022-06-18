using Faze.Abstractions.GameMoves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Core.Extensions
{
    public static class GridMoveExtensions
    {
        public static int GetX(this GridMove move, int dimension)
        {
            return move % dimension;
        }

        public static int GetY(this GridMove move, int dimension)
        {
            return move / dimension;
        }

        public static GridMove ToSubdimension(this GridMove move, int dimension, int newDimension)
        {
            var x = move.GetX(dimension);
            var y = move.GetY(dimension);

            var f = (double)newDimension / dimension;

            var newX = (int)(x * f);
            var newY = (int)(y * f);

            return new GridMove(newX, newY, newDimension);
        }

        public static IEnumerable<GridMove> GetRow(this GridMove pos, int dimension)
        {
            var y = pos.GetY(dimension);
            return Enumerable.Range(0, dimension).Select(i => new GridMove(i, y, dimension));
        }

        public static IEnumerable<GridMove> GetColumn(this GridMove pos, int dimension)
        {
            var x = pos.GetX(dimension);
            return Enumerable.Range(0, dimension).Select(i => new GridMove(x, i, dimension));
        }        

        public static IEnumerable<GridMove> GetDiagonals(this GridMove pos, int dimension)
        {
            var x = pos.GetX(dimension);
            var y = pos.GetY(dimension);
            (int dx, int dy) direction = (1, 1);

            var minPos = Math.Min(x, y);
            (int x, int y) start = (x - minPos, y - minPos);
            var limit = dimension - Math.Max(start.x, start.y);
            for (var i = 0; i < limit; i++)
            {
                yield return new GridMove(start.x + direction.dx * i, start.y + direction.dy * i, dimension);
            }

            direction = (1, -1);
            start = x + y < dimension - 1
                ? (0, y + x)
                : (x - (dimension - 1 - y), dimension - 1);

            limit = Math.Min(start.y + 1, dimension - start.x);
            for (var i = 0; i < limit; i++)
            {
                yield return new GridMove(start.x + direction.dx * i, start.y + direction.dy * i, dimension);
            }
        }

        public static IEnumerable<GridMove> GetBox(this GridMove pos, int dimension, int subdimension)
        {
            var subpos = pos.ToSubdimension(dimension, subdimension).ToSubdimension(subdimension, dimension);
            var subtilesCount = subdimension * subdimension;

            var xOffset = subpos.GetX(dimension);
            var yOffset = subpos.GetY(dimension);

            return Enumerable.Range(0, subtilesCount).Select(i =>
            {
                var gm = new GridMove(i);
                var x = gm.GetX(subdimension);
                var y = gm.GetY(subdimension);

                return new GridMove(x + xOffset, y + yOffset, dimension);
            });
        }
    }
}
