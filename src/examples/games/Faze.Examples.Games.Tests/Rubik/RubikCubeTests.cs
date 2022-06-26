using Faze.Abstractions.GameMoves;
using Faze.Examples.Games.Rubik;
using Shouldly;
using System.Linq;
using Xunit;

namespace Faze.Examples.Games.Tests.Rubik
{
    public class RubikCubeTests
    {
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
        [InlineData("F B L", "L' B' F'")]
        [InlineData("F B L R U D", "D' U' R' L' B' F'")]
        [InlineData("F U F R F D F L F B", "B' F' L' F' D' F' R' F' U' F'")]
        public void CanMixAndUndo(string mixSequence, string undoSequence)
        {
            var summary = $"{mixSequence} -> {undoSequence}";

            // fake cube
            var originalCubeNotation = "R|ROGBWYBY,O|ROGBWYBY,G|ROGBWYBY,B|ROGBWYBY,W|ROGBWYBY,Y|ROGBWYBY";
            var cube = RubikNotationExtensions.ParseCube(originalCubeNotation);

            cube = RubikTestUtils.RunMoves(cube, mixSequence);
            cube = RubikTestUtils.RunMoves(cube, undoSequence);

            cube.ToNotation().ShouldBe(originalCubeNotation, summary);
        }

        [Theory]
        [InlineData("F", "R|RRRRRRRR,O|OOOOOOOO,G|GGYYYGGG,B|WBBBBBWW,W|WWWWGGGW,Y|BBBYYYYY")]
        [InlineData("F R", "R|RRBYYRRR,O|GOOOOOWW,G|GGYYYGGG,B|WWWBBBBB,W|WWRRRGGW,Y|BBOOOYYY")]
        [InlineData("U R", "R|BBYYYRRR,O|WGGOOOWW,G|RRRGGGGG,B|BBOOOBBB,W|WWBRRWWW,Y|YYOOGYYY")]
        [InlineData("U R R'", "R|BBBRRRRR,O|GGGOOOOO,G|RRRGGGGG,B|OOOBBBBB,W|WWWWWWWW,Y|YYYYYYYY")]
        [InlineData("F B L", "R|BRRRRRGW,O|OOGYBOOO,G|WWWGYYYG,B|WBYYYBWW,W|OBBWGGOO,Y|RBBYGGRR")]
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
