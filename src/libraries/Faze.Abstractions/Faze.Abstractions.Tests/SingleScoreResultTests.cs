using Faze.Abstractions.GameResults;
using Shouldly;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class SingleScoreResultTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void CanImplicitCastToInt(int score)
        {
            var result = new SingleScoreResult(score);
            int scoreInt = result;

            scoreInt.ShouldBe(score);
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(1, "1")]
        [InlineData(5, "5")]
        [InlineData(100, "100")]
        public void CorrectToString(int score, string expectedString)
        {
            var result = new SingleScoreResult(score);

            result.ToString().ShouldBe(expectedString);
        }
    }

}
