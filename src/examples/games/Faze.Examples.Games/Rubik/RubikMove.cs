using System;

namespace Faze.Examples.Games.Rubik
{
    public struct RubikMove
    {
        private const char primeNotation = '\'';

        public RubikMove(RubikMoveFace face, RubikMoveDirection direction = RubikMoveDirection.Clockwise)
        {
            Face = face;
            Direction = direction;
        }

        public RubikMoveFace Face { get; }
        public RubikMoveDirection Direction { get; }

        public string ToNotation()
        {
            var faceNotation = GetFaceNotation(Face);
            return Direction == RubikMoveDirection.Clockwise
                ? faceNotation
                : faceNotation + primeNotation;
        }

        public static RubikMove Parse(string notation)
        {
            var faceNotation = notation.Substring(0, 1);
            var face = GetFaceFromNotation(faceNotation);

            if (notation.Length == 1)
                return new RubikMove(face);

            if (notation.Length == 2 && notation[1] == primeNotation)
                return new RubikMove(face, RubikMoveDirection.Anticlockwise);

            throw new NotSupportedException($"Unknown move notation '{notation}'");
        }

        private static string GetFaceNotation(RubikMoveFace face)
        {
            return face.ToString().Substring(0, 1);
        }

        private static RubikMoveFace GetFaceFromNotation(string notation)
        {
            switch (notation)
            {
                case "F":
                    return RubikMoveFace.Front;
                case "B":
                    return RubikMoveFace.Back;
                case "L":
                    return RubikMoveFace.Left;
                case "R":
                    return RubikMoveFace.Right;
                case "U":
                    return RubikMoveFace.Up;
                case "D":
                    return RubikMoveFace.Down;
            }

            throw new NotSupportedException($"Unknown face notation '{notation}'");
        }
    }
}
