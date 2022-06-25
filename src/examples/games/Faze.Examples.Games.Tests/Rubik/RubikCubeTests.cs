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

        [Theory]
        [InlineData("F", "F'")]
        [InlineData("B", "B'")]
        [InlineData("L", "L'")]
        [InlineData("R", "R'")]
        [InlineData("U", "U'")]
        [InlineData("D", "D'")]
        [InlineData("F B", "B' F'")]
        [InlineData("F B", "F' B'")]
        [InlineData("F R", "R' F'")]
        [InlineData("U R", "R' U'")]
        [InlineData("F B L", "L' B' F")]
        [InlineData("F B L R U D", "D' U' R' L' B' F'")]
        public void CanMixAndUndo(string mixSequence, string undoSequence)
        {
            var summary = $"{mixSequence} -> {undoSequence}";

            var cube = RubikCube.Solved();
            cube.IsSolved().ShouldBeTrue(summary);

            cube = RubikTestUtils.RunMoves(cube, mixSequence);
            cube.IsSolved().ShouldBeFalse(summary);

            cube = RubikTestUtils.RunMoves(cube, undoSequence);
            cube.IsSolved().ShouldBeTrue(summary);
        }

        [Theory]
        [InlineData("F", "R|RRRRRRRR,O|OOOOOOOO,G|GGYYYGGG,B|WBBBBBWW,W|WWWWGGGW,Y|BBBYYYYY")]
        [InlineData("F R", "R|RRBYYRRR,O|GOOOOOWW,G|GGYYYGGG,B|WWWBBBBB,W|WWRRRGGW,Y|BBOOOYYY")]
        public void CanMixAndCorrectState(string mixSequence, string expectedCubeNotation)
        {
            var cube = RubikCube.Solved();
            cube.IsSolved().ShouldBeTrue();

            cube = RubikTestUtils.RunMoves(cube, mixSequence);
            cube.ToNotation().ShouldBe(expectedCubeNotation, mixSequence);
        }

        [Fact]
        public void GetCubeFromPerspective()
        {
            // fake cube
            var cube = RubikNotationExtensions.ParseCube("R|YRRRRRRR,O|ROOOOOOO,G|OGGGGGGG,B|GBBBBBBB,W|BWWWWWWW,Y|WYYYYYYY");

            // no change from front
            var cubeFromFront = cube.GetCubeFromPerspective(RubikMoveFace.Front);
            cubeFromFront.ShouldBe(RubikNotationExtensions.ParseCube("R|YRRRRRRR,O|ROOOOOOO,G|OGGGGGGG,B|GBBBBBBB,W|BWWWWWWW,Y|WYYYYYYY"));

            var cubeFromBack = cube.GetCubeFromPerspective(RubikMoveFace.Back);
            cubeFromBack.ShouldBe(RubikNotationExtensions.ParseCube("O|ROOOOOOO,R|YRRRRRRR,B|GBBBBBBB,G|OGGGGGGG,W|WWWWBWWW,Y|YYYYWYYY"));

            var cubeFromLeft = cube.GetCubeFromPerspective(RubikMoveFace.Left);
            cubeFromLeft.ShouldBe(RubikNotationExtensions.ParseCube("G|OGGGGGGG,B|GBBBBBBB,O|ROOOOOOO,R|YRRRRRRR,W|WWWWWWBW,Y|YYWYYYYY"));

            var cubeFromRight = cube.GetCubeFromPerspective(RubikMoveFace.Right);
            cubeFromRight.ShouldBe(RubikNotationExtensions.ParseCube("B|GBBBBBBB,G|OGGGGGGG,R|YRRRRRRR,O|ROOOOOOO,W|WWBWWWWW,Y|YYYYYYWY"));

            var cubeFromUp = cube.GetCubeFromPerspective(RubikMoveFace.Up);
            cubeFromUp.ShouldBe(RubikNotationExtensions.ParseCube("W|BWWWWWWW,Y|YYYYWYYY,G|GGOGGGGG,B|BBBBBBGB,O|OOOOROOO,R|YRRRRRRR"));

            var cubeFromDown = cube.GetCubeFromPerspective(RubikMoveFace.Down);
            cubeFromDown.ShouldBe(RubikNotationExtensions.ParseCube("Y|WYYYYYYY,W|WWWWBWWW,G|GGGGGGOG,B|BBGBBBBB,R|YRRRRRRR,O|OOOOROOO"));

        }

        [Fact]
        public void CubeNotation()
        {
            var expectedNotation = "R|RRRRRRRR,O|OOOOOOOO,G|GGYYYGGG,B|WBBBBBWW,W|WWWWGGGW,Y|BBBYYYYY";
            var cube = RubikNotationExtensions.ParseCube(expectedNotation);
            cube.ToNotation().ShouldBe(expectedNotation);        
        }
    }
}
