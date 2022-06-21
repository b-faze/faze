using System.Linq;

namespace Faze.Examples.Games.Rubik
{
    internal class RubikFace 
    {
        private RubikColor centerColor;

        public static RubikFace Solved(RubikColor centerColor) =>
            new RubikFace(centerColor,
                RubikFaceSide.Initial(centerColor),
                RubikFaceSide.Initial(centerColor),
                RubikFaceSide.Initial(centerColor),
                RubikFaceSide.Initial(centerColor));

        private RubikFace(RubikColor centerColor, RubikFaceSide top, RubikFaceSide right, RubikFaceSide bottom, RubikFaceSide left)
        {
            this.centerColor = centerColor;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Left = left;
        }

        public RubikFaceSide Top { get; }
        public RubikFaceSide Right { get; }
        public RubikFaceSide Bottom { get; }
        public RubikFaceSide Left { get; }

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

        private RubikFace RotateClockwise()
        {
            return new RubikFace(centerColor, Left, Top, Right, Bottom);
        }

        private RubikFace RotateAniclockwise()
        {
            return new RubikFace(centerColor, Right, Bottom, Left, Top);
        }

        public RubikFace SetTop(RubikFaceSide newTop) => new RubikFace(centerColor, newTop, Right, Bottom, Left);
        public RubikFace SetRight(RubikFaceSide newRight) => new RubikFace(centerColor, Top, newRight, Bottom, Left);
        public RubikFace SetBottom(RubikFaceSide newBottom) => new RubikFace(centerColor, Top, Right, newBottom, Left);
        public RubikFace SetLeft(RubikFaceSide newLeft) => new RubikFace(centerColor, Top, Right, Bottom, newLeft);
    }
}
