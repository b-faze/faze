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
        public RubikFace Up { get; }
        public RubikFace Down { get; }

        public static RubikCube Solved() => new RubikCube(
            front: RubikFace.Solved(RubikColor.Red),
            back: RubikFace.Solved(RubikColor.Orange),
            left: RubikFace.Solved(RubikColor.Green),
            right: RubikFace.Solved(RubikColor.Blue),
            up: RubikFace.Solved(RubikColor.White),
            down: RubikFace.Solved(RubikColor.Yellow)
        );

        public RubikCube(RubikFace front, RubikFace back, RubikFace left, RubikFace right, RubikFace up, RubikFace down)
        {
            Front = front;
            Back = back;
            Left = left;
            Right = right;
            Up = up;
            Down = down;
        }

        public bool IsSolved()
        {
            return Front.IsSolved()
                && Back.IsSolved()
                && Left.IsSolved()
                && Right.IsSolved()
                && Up.IsSolved()
                && Down.IsSolved();
        }

        public RubikCube Move(RubikMove move)
        {
            var relativeCube = GetCubeFromPerspective(move.Face);
            var newRelativeCube = move.Direction == RubikMoveDirection.Clockwise
                ? relativeCube.RotateClockwise(move.Direction)
                : relativeCube.RotateAnticlockwise(move.Direction);

            var newCube = newRelativeCube.GetCubeFromPerspective(InvertPerspective(move.Face));
            return newCube;
        }

        private RubikCube RotateClockwise(RubikMoveDirection direction) 
        {
            var newFront = Front.Rotate(direction);
            var newTop = Up.SetBottom(Left.Right.ToArray());
            var newRight = Right.SetLeft(Up.Bottom.ToArray());
            var newBottom = Down.SetTop(Right.Left.ToArray());
            var newLeft = Left.SetRight(Down.Top.ToArray());

            return new RubikCube(newFront, Back, newLeft, newRight, newTop, newBottom);
        }

        private RubikCube RotateAnticlockwise(RubikMoveDirection direction)
        {
            var newFront = Front.Rotate(direction);
            var newTop = Up.SetBottom(Right.Left.ToArray());
            var newRight = Right.SetLeft(Down.Top.ToArray());
            var newBottom = Down.SetTop(Left.Right.ToArray());
            var newLeft = Left.SetRight(Up.Bottom.ToArray());

            return new RubikCube(newFront, Back, newLeft, newRight, newTop, newBottom);
        }

        /// <summary>
        /// Creates a new cube from the perspective of the given face
        /// </summary>
        public RubikCube GetCubeFromPerspective(RubikMoveFace face) 
        {
            switch (face)
            {
                case RubikMoveFace.Front:
                    return new RubikCube(Front, Back, Left, Right, Up, Down);

                case RubikMoveFace.Back:
                    return new RubikCube(
                        front: Back, 
                        back: Front, 
                        left: Right, 
                        right: Left, 
                        up: Up.Rotate180(), 
                        down: Down.Rotate180());

                case RubikMoveFace.Left:
                    return new RubikCube(
                        front: Left, 
                        back: Right, 
                        left: Back, 
                        right: Front, 
                        up: Up.RotateAniclockwise(), 
                        down: Down.RotateClockwise());

                case RubikMoveFace.Right:
                    return new RubikCube(
                        front: Right, 
                        back: Left, 
                        left: Front, 
                        right: Back, 
                        up: Up.RotateClockwise(), 
                        down: Down.RotateAniclockwise());

                case RubikMoveFace.Up:
                    return new RubikCube(
                        front: Up, 
                        back: Down.Rotate180(), 
                        left: Left.RotateClockwise(), 
                        right: Right.RotateAniclockwise(), 
                        up: Back.Rotate180(), 
                        down: Front);

                case RubikMoveFace.Down:
                    return new RubikCube(
                        front: Down, 
                        back: Up.Rotate180(), 
                        left: Left.RotateAniclockwise(), 
                        right: Right.RotateClockwise(), 
                        up: Front, 
                        down: Back.Rotate180());
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

                case RubikMoveFace.Up:
                    return RubikMoveFace.Down;

                case RubikMoveFace.Down:
                    return RubikMoveFace.Up;
            }

            throw new NotSupportedException($"Unknown face '{face}'");
        }
    }
}
