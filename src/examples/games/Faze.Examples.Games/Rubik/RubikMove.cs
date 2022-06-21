namespace Faze.Examples.Games.Rubik
{
    public struct RubikMove
    {
        public RubikMove(RubikMoveFace face, RubikMoveDirection direction)
        {
            Face = face;
            Direction = direction;
        }

        public RubikMoveFace Face { get; }
        public RubikMoveDirection Direction { get; }
    }
}
