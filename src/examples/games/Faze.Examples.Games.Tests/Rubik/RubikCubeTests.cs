using Faze.Abstractions.GameMoves;
using Faze.Examples.Games.Rubik;
using Shouldly;
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

            cube.Top.IsSolved().ShouldBeFalse("top unsolved");
            cube.Top.Bottom.ShouldAllBe(c => c == cube.Left.Center, "left => top");

            cube.Bottom.IsSolved().ShouldBeFalse("bottom unsolved");
            cube.Bottom.Top.ShouldAllBe(c => c == cube.Right.Center, "Right => Bottom");

            cube.Left.IsSolved().ShouldBeFalse("left unsolved");
            cube.Left.Right.ShouldAllBe(c => c == cube.Bottom.Center, "Bottom => Left");

            cube.Right.IsSolved().ShouldBeFalse("right unsolved");
            cube.Right.Left.ShouldAllBe(c => c == cube.Top.Center, "Top => Right");
        }

        [Fact]
        public void CanRotateAnticlockwise()
        {
            var cube = RubikCube.Solved();
            cube = cube.Move(new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Anticlockwise));
            cube.Front.IsSolved().ShouldBeTrue("no change to rotated face");

            // other faces updated
            cube.Back.IsSolved().ShouldBeTrue("back remains solved");

            cube.Top.IsSolved().ShouldBeFalse("top unsolved");
            cube.Top.Bottom.ShouldAllBe(c => c == cube.Right.Center, "right => top");

            cube.Bottom.IsSolved().ShouldBeFalse("bottom unsolved");
            cube.Bottom.Top.ShouldAllBe(c => c == cube.Left.Center, "Left => Bottom");

            cube.Left.IsSolved().ShouldBeFalse("left unsolved");
            cube.Left.Right.ShouldAllBe(c => c == cube.Top.Center, "Top => Left");

            cube.Right.IsSolved().ShouldBeFalse("right unsolved");
            cube.Right.Left.ShouldAllBe(c => c == cube.Bottom.Center, "Bottom => Right");
        }

        [Fact]
        public void CanMixAndUndo()
        {
            var cube = RubikCube.Solved();
            cube.IsSolved().ShouldBeTrue();

            cube = cube.Move(new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Clockwise));
            cube.IsSolved().ShouldBeFalse();

            cube = cube.Move(new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Anticlockwise));
            cube.IsSolved().ShouldBeTrue();
        }
    }
}
