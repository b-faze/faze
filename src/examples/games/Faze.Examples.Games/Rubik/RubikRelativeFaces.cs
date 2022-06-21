namespace Faze.Examples.Games.Rubik
{
    internal class RubikRelativeFaces
    {
        public RubikRelativeFaces(RubikFace front, RubikFace top, RubikFace right, RubikFace bottom, RubikFace left)
        {
            Front = front;
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public RubikFace Front { get; }
        public RubikFace Top { get; }
        public RubikFace Right { get; }
        public RubikFace Bottom { get; }
        public RubikFace Left { get; }

        public RubikRelativeFaces Rotate(RubikMoveDirection direction)
        {
            var newFront = Front.Rotate(direction);
            var newTop = Top.SetBottom(newFront.Top);
            var newRight = Right.SetLeft(newFront.Right);
            var newBottom = Left.SetTop(newFront.Bottom);
            var newLeft = Left.SetRight(newFront.Left);

            return new RubikRelativeFaces(newFront, newTop, newRight, newBottom, newLeft);
        }
    }
}
