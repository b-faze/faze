using System;

namespace Faze.Examples.Games.Rubik
{
    internal class RubikCube
    {
        public RubikFace Front { get; }
        public RubikFace Back { get; }
        public RubikFace Left { get; }
        public RubikFace Right { get; }
        public RubikFace Top { get; }
        public RubikFace Bottom { get; }

        public static RubikCube Solved() => new RubikCube(
            front: RubikFace.Solved(RubikColor.Red),
            back: RubikFace.Solved(RubikColor.Purple),
            left: RubikFace.Solved(RubikColor.Yellow),
            right: RubikFace.Solved(RubikColor.Green),
            top: RubikFace.Solved(RubikColor.Blue),
            bottom: RubikFace.Solved(RubikColor.White)
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

        public RubikCube Move(RubikMove move)
        {
            var relativeCube = GetCubeFromPerspective(move.Face);
            var newRelativeCube = relativeCube.Rotate(move.Direction);

            return GetCubeFromPerspective(RubikMoveFace.Front); // TODO: need to fix by inverting
        }

        private RubikCube Rotate(RubikMoveDirection direction) 
        {
            var newFront = Front.Rotate(direction);
            var newTop = Top.SetBottom(newFront.Top);
            var newRight = Right.SetLeft(newFront.Right);
            var newBottom = Bottom.SetTop(newFront.Bottom);
            var newLeft = Left.SetRight(newFront.Left);

            return new RubikCube(newFront, Back, newLeft, newRight, newTop, newBottom);
        }

        /// <summary>
        /// Creates a new cube from the perspective of a new face
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
        }
    }
}
