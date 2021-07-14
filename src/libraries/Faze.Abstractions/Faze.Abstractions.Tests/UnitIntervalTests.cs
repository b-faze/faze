using Faze.Abstractions.Core;
using Shouldly;
using System;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class UnitIntervalTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(0.5)]
        [InlineData(1)]
        public void AcceptsValidInput(double validInput)
        {
            var ui = new UnitInterval(validInput);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.5)]
        [InlineData(1)]
        public void CanImplicitCastForDouble(double originalInput)
        {
            double ui = new UnitInterval(originalInput);
            ui.ShouldBe(originalInput);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        [InlineData(-200)]
        [InlineData(200)]
        public void CanRejectInvalidInputs(double invalidInput)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new UnitInterval(invalidInput));
        }
    }
}
