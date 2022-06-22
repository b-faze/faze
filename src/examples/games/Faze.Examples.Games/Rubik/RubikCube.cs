using System;
using System.Linq;

namespace Faze.Examples.Games.Rubik
{
    public class RubikCube
    {
        public RubikFace Front { get; }
        public RubikFace Back { get; }
        public RubikFace Left { get; }
        public RubikFace Right { get; }
        public RubikFace Top { get; }
        public RubikFace Bottom { get; }

        public static RubikCube Solved() => new RubikCube(
            front: RubikFace.Solved(RubikColor.R),
            back: RubikFace.Solved(RubikColor.P),
            left: RubikFace.Solved(RubikColor.Y),
            right: RubikFace.Solved(RubikColor.G),
            top: RubikFace.Solved(RubikColor.B),
            bottom: RubikFace.Solved(RubikColor.W)
        );

        private RubikCube(RubikFace front, RubikFace back, RubikFace left, RubikFace right, RubikFace top, RubikFace bottom)
        {
            Front = front;
            Back = back;
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public bool IsSolved()
        {
            return Front.IsSolved()
                && Back.IsSolved()
                && Left.IsSolved()
                && Right.IsSolved()
                && Top.IsSolved()
                && Bottom.IsSolved();
        }

        public RubikCube Move(RubikMove move)
        {
            var relativeCube = GetCubeFromPerspective(move.Face);
            var newRelativeCube = move.Direction == RubikMoveDirection.Clockwise
                ? relativeCube.RotateClockwise(move.Direction)
                : relativeCube.RotateAnticlockwise(move.Direction);

            return newRelativeCube.GetCubeFromPerspective(InvertPerspective(move.Face));
        }

        public override string ToString()
        {
            return $"Front[{Front}],\nBack[{Back}],\nLeft[{Left}],\nRight[{Right}],\nTop[{Top}],\nBottom[{Bottom}]";
        }

        private RubikCube RotateClockwise(RubikMoveDirection direction) 
        {
            var newFront = Front.Rotate(direction);
            var newTop = Top.SetBottom(Left.Right.ToArray());
            var newRight = Right.SetLeft(Top.Bottom.ToArray());
            var newBottom = Bottom.SetTop(Right.Left.ToArray());
            var newLeft = Left.SetRight(Bottom.Top.ToArray());

            return new RubikCube(newFront, Back, newLeft, newRight, newTop, newBottom);
        }

        private RubikCube RotateAnticlockwise(RubikMoveDirection direction)
        {
            var newFront = Front.Rotate(direction);
            var newTop = Top.SetBottom(Right.Left.ToArray());
            var newRight = Right.SetLeft(Bottom.Top.ToArray());
            var newBottom = Bottom.SetTop(Left.Right.ToArray());
            var newLeft = Left.SetRight(Top.Bottom.ToArray());

            return new RubikCube(newFront, Back, newLeft, newRight, newTop, newBottom);
        }

        /// <summary>
        /// Creates a new cube from the perspective of the given face
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        private RubikCube GetCubeFromPerspective(RubikMoveFace face) 
        {
            switch (face)
            {
                case RubikMoveFace.Front:
                    return new RubikCube(Front, Back, Left, Right, Top, Bottom);

                case RubikMoveFace.Back:
                    return new RubikCube(Back, Front, Right, Left, Top, Bottom);

                case RubikMoveFace.Left:
                    return new RubikCube(Left, Right, Back, Front, Top, Bottom);

                case RubikMoveFace.Right:
                    return new RubikCube(Right, Left, Front, Back, Top, Bottom);

                case RubikMoveFace.Top:
                    return new RubikCube(Top, Bottom, Right, Left, Back, Front);

                case RubikMoveFace.Bottom:
                    return new RubikCube(Bottom, Top, Right, Left, Front, Back);
            }

            throw new NotSupportedException($"Unknown face '{face}'");
        }

        private static RubikMoveFace InvertPerspective(RubikMoveFace face)
        {
            switch (face)
            {
                case RubikMoveFace.Front:
                    return RubikMoveFace.Front;

                case RubikMoveFace.Back:
                    return RubikMoveFace.Back;

                case RubikMoveFace.Left:
                    return RubikMoveFace.Right;

                case RubikMoveFace.Right:
                    return RubikMoveFace.Left;

                case RubikMoveFace.Top:
                    return RubikMoveFace.Bottom;

                case RubikMoveFace.Bottom:
                    return RubikMoveFace.Top;
            }

            throw new NotSupportedException($"Unknown face '{face}'");
        }
    }
}
