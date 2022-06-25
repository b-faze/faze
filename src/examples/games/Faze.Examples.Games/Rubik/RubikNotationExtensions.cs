using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Examples.Games.Rubik
{
    public static class RubikNotationExtensions
    {
        private const char RubikFaceSeparator = ',';
        private const char RubikFaceCenterSeparator = '|';

        public static string ToNotation(this RubikCube cube)
        {
            var faces = new[]
            {
                cube.Front,
                cube.Back,
                cube.Left,
                cube.Right,
                cube.Up,
                cube.Down,
            };

            return string.Join($"{RubikFaceSeparator}", faces.Select(ToNotation));
        }
        public static RubikCube ParseCube(string notation)
        {
            var faces = notation.Split(RubikFaceSeparator).Select(ParseFace).ToArray();
            return new RubikCube(faces[0], faces[1], faces[2], faces[3], faces[4], faces[5]);
        }

        public static string ToNotation(this RubikFace face)
        {
            var edgeNotation = string.Join("", face.Edge.Select(c => c.ToNotation()));
            return $"{face.Center.ToNotation()}{RubikFaceCenterSeparator}{edgeNotation}";
        }

        public static RubikFace ParseFace(string notation)
        {
            var parts = notation.Split(RubikFaceCenterSeparator);
            var centerPart = parts[0];
            var edgePart = parts[1];
            return new RubikFace(ParseColor(centerPart), edgePart.Select(n => ParseColor($"{n}")));
        }

        public static string ToNotation(this RubikColor color) 
        {
            return color.ToString().Substring(0, 1);
        }

        public static RubikColor ParseColor(string notation)
        {
            switch (notation)
            {
                case "R":
                    return RubikColor.Red;
                case "O":
                    return RubikColor.Orange;
                case "G":
                    return RubikColor.Green;
                case "B":
                    return RubikColor.Blue;
                case "W":
                    return RubikColor.White;
                case "Y":
                    return RubikColor.Yellow;
            }

            throw new NotSupportedException($"Unknown color notation '{notation}'");
        }
    }
}
