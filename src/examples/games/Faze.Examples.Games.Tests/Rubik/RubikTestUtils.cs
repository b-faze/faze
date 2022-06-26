using Faze.Examples.Games.Rubik;
using Shouldly;

namespace Faze.Examples.Games.Tests.Rubik
{
    public static class RubikTestUtils
    {
        public static RubikCube RunMoves(RubikCube cube, string moveSequence)
        {
            foreach (var move in RubikUtils.ParseMoves(moveSequence))
            {
                cube = cube.Move(move);
            }

            return cube;
        }

        public static void ShouldBe(this RubikCube cube, RubikCube expected)
        {
            cube.Front.ShouldBe(expected.Front);
            cube.Back.ShouldBe(expected.Back);
            cube.Left.ShouldBe(expected.Left);
            cube.Right.ShouldBe(expected.Right);
            cube.Up.ShouldBe(expected.Up);
            cube.Down.ShouldBe(expected.Down);
        }

        public static void ShouldBe(this RubikFace face, RubikFace expected)
        {
            face.ToNotation().ShouldBe(expected.ToNotation());
        }
    }
}
