using Faze.Abstractions.GameMoves;
using Faze.Examples.Games.Rubik;
using Shouldly;
using System.Linq;
using Xunit;

namespace Faze.Examples.Games.Tests.Rubik
{
    public class RubikCubeTests
    {
        [Fact]
        public void CanRotateClockwise()
        {
            var cube = RubikCube.Solved();
            cube = cube.Move(new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Clockwise));
            cube.Front.IsSolved().ShouldBeTrue("no change to rotated face");

            // other faces updated
            cube.Back.IsSolved().ShouldBeTrue("back remains solved");

            cube.Up.IsSolved().ShouldBeFalse("top unsolved");
            cube.Up.Bottom.ShouldAllBe(c => c == cube.Left.Center, "left => top");

            cube.Down.IsSolved().ShouldBeFalse("bottom unsolved");
            cube.Down.Top.ShouldAllBe(c => c == cube.Right.Center, "Right => Bottom");

            cube.Left.IsSolved().ShouldBeFalse("left unsolved");
            cube.Left.Right.ShouldAllBe(c => c == cube.Down.Center, "Bottom => Left");

            cube.Right.IsSolved().ShouldBeFalse("right unsolved");
            cube.Right.Left.ShouldAllBe(c => c == cube.Up.Center, "Top => Right");
        }

        [Fact]
        public void CanRotateAnticlockwise()
        {
            var cube = RubikCube.Solved();
            cube = cube.Move(new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Anticlockwise));
            cube.Front.IsSolved().ShouldBeTrue("no change to rotated face");

            // other faces updated
            cube.Back.IsSolved().ShouldBeTrue("back remains solved");

            cube.Up.IsSolved().ShouldBeFalse("top unsolved");
            cube.Up.Bottom.ShouldAllBe(c => c == cube.Right.Center, "right => top");

            cube.Down.IsSolved().ShouldBeFalse("bottom unsolved");
            cube.Down.Top.ShouldAllBe(c => c == cube.Left.Center, "Left => Bottom");

            cube.Left.IsSolved().ShouldBeFalse("left unsolved");
            cube.Left.Right.ShouldAllBe(c => c == cube.Up.Center, "Top => Left");

            cube.Right.IsSolved().ShouldBeFalse("right unsolved");
            cube.Right.Left.ShouldAllBe(c => c == cube.Down.Center, "Bottom => Right");
        }

        [Fact]
        public void CanMixAndUndo()
        {
            var faceSequence = new RubikMoveFace[]
            {
                RubikMoveFace.Front,
                RubikMoveFace.Left,
                RubikMoveFace.Back,
            };

            var cube = RubikCube.Solved();
            cube.IsSolved().ShouldBeTrue();

            // mix
            foreach (var face in faceSequence)
            {
                cube = cube.Move(new RubikMove(face, RubikMoveDirection.Clockwise));
            }
            cube.IsSolved().ShouldBeFalse();

            // reverse mix
            foreach (var face in faceSequence.Reverse())
            {
                cube = cube.Move(new RubikMove(face, RubikMoveDirection.Anticlockwise));
            }
            cube.IsSolved().ShouldBeTrue();
        }

        [Fact]
        public void CanMixClockwise()
        {
            var expectedCube = RubikNotationExtensions.ParseCube("R|RGYYYRWW,O|WWYYBOWW,G|GGGOOYYG,B|RRBBBBBB,W|ORRGGWWO,Y|GBOOOYRR");
            //cube = RubikTestUtils.RunMoves(cube, "F R U' L");

            var cube = RubikCube.Solved();
            cube.IsSolved().ShouldBeTrue();

            // mix

            cube = cube.Move(RubikMove.Parse("F"));
            cube.ShouldBe(RubikNotationExtensions.ParseCube("R|RRRRRRRR,O|OOOOOOOO,G|GGYYYGGG,B|WBBBBBWW,W|WWWWGGGW,Y|BBBYYYYY"));

            cube = cube.Move(RubikMove.Parse("R"));
            cube.ShouldBe(RubikNotationExtensions.ParseCube("R|RRBYYRRR,O|GOOOOOWW,G|GGYYYGGG,B|WWWBBBBB,W|WWRRRGGW,Y|BBOOOYYY"));
        }
    }
}
