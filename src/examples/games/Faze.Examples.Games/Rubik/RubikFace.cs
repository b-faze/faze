using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.Rubik
{
    public class RubikFace 
    {
        private RubikColor[] edge;
        public static RubikFace Solved(RubikColor centerColor) =>
            new RubikFace(centerColor, Enumerable.Range(0, 8).Select(i => centerColor));

        public RubikFace(RubikColor center, IEnumerable<RubikColor> edge)
        {
            this.Center = center;
            this.edge = edge.ToArray();
        }

        public RubikColor Center { get; }
        /// <summary>
        /// Top -> Right -> Bottom -> Left
        /// </summary>
        public IEnumerable<RubikColor> Edge => edge;
        public IEnumerable<RubikColor> Top => new RubikColor[] { edge[0], edge[1], edge[2] };
        public IEnumerable<RubikColor> Right => new RubikColor[] { edge[2], edge[3], edge[4] };
        public IEnumerable<RubikColor> Bottom => new RubikColor[] { edge[4], edge[5], edge[6] };
        public IEnumerable<RubikColor> Left => new RubikColor[] { edge[6], edge[7], edge[0] };



        public bool IsSolved()
        {
            return Edge.All(c => c == Center);
        }

        public RubikFace Rotate180()
        {
            return RotateClockwise().RotateClockwise();
        }

        public RubikFace Rotate(RubikMoveDirection direction)
        {
            if (direction == RubikMoveDirection.Clockwise)
            {
                return RotateClockwise();
            }
            else
            {
                return RotateAniclockwise();
            }
        }

        public RubikFace RotateClockwise()
        {
            var newEdge = new RubikColor[edge.Length];
            for (var i = 0; i < edge.Length; i++)
            {
                newEdge[(i + 2) % edge.Length] = edge[i];
            }
            return new RubikFace(Center, newEdge);
        }

        public RubikFace RotateAniclockwise()
        {
            var newEdge = new RubikColor[edge.Length];
            for (var i = 0; i < edge.Length; i++)
            {
                newEdge[i] = edge[(i + 2) % edge.Length];
            }
            return new RubikFace(Center, newEdge);
        }

        public RubikFace SetTop(RubikColor[] e) 
            => new RubikFace(Center, new RubikColor[] { e[0], e[1], e[2], edge[3], edge[4], edge[5], edge[6], edge[7] });
        public RubikFace SetRight(RubikColor[] e)
            => new RubikFace(Center, new RubikColor[] { edge[0], edge[1], e[0], e[1], e[2], edge[5], edge[6], edge[7] });
        public RubikFace SetBottom(RubikColor[] e)
            => new RubikFace(Center, new RubikColor[] { edge[0], edge[1], edge[2], edge[3], e[0], e[1], e[2], edge[7] });
        public RubikFace SetLeft(RubikColor[] e)
            => new RubikFace(Center, new RubikColor[] { e[0], edge[1], edge[2], edge[3], edge[4], edge[5], e[1], e[2] });
    }
}
