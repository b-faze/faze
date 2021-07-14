using Faze.Abstractions.GameMoves;
using Shouldly;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class GridMoveTests
    {
        [Fact]
        public void CanCreateEmpty()
        {
            var move = new GridMove();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void CanCreateFromIndex(int index)
        {
            int move = new GridMove(index);

            move.ShouldBe(index);
        }

        [Theory]
        [InlineData(0, 1, 3, 3)]
        public void CanCreateFromCoordinates(int x, int y, int w, int expectedIndex)
        {
            int move = new GridMove(x, y, w);

            move.ShouldBe(expectedIndex);
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(1, "1")]
        [InlineData(5, "5")]
        [InlineData(100, "100")]
        public void CorrectToString(int index, string expectedString)
        {
            GridMove move = new GridMove(index);

            move.ToString().ShouldBe(expectedString);
        }
    }

}
