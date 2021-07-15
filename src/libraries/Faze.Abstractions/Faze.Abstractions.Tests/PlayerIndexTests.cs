using Faze.Abstractions.GameMoves;
using Faze.Abstractions.Players;
using Shouldly;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class PlayerIndexTests
    {
        [Fact]
        public void CorrectConstants()
        {
            PlayerIndex.P1.ShouldBe(new PlayerIndex(0));
            PlayerIndex.P2.ShouldBe(new PlayerIndex(1));
        }

        [Fact]
        public void CanCreateEmpty()
        {
            int index = new PlayerIndex();
            index.ShouldBe(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void CanCreateFromIndex(int index)
        {
            int move = new PlayerIndex(index);

            move.ShouldBe(index);
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(1, "1")]
        [InlineData(5, "5")]
        [InlineData(100, "100")]
        public void CorrectToString(int index, string expectedString)
        {
            PlayerIndex move = new PlayerIndex(index);

            move.ToString().ShouldBe(expectedString);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void UsesSameHashcodeAsIndex(int index)
        {
            new PlayerIndex(index).GetHashCode().ShouldBe(index.GetHashCode());
        }
    }

}
