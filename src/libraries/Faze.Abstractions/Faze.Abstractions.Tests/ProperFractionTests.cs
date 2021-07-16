using Faze.Abstractions.Core;
using Shouldly;
using System;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class ProperFractionTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(0.5)]
        [InlineData(0.99)]
        public void AcceptsValidInput(double validInput)
        {
            var ui = new ProperFraction(validInput);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.5)]
        [InlineData(0.99)]
        public void CanImplicitCastTDouble(double originalInput)
        {
            double ui = new ProperFraction(originalInput);
            ui.ShouldBe(originalInput);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.5)]
        [InlineData(0.99)]
        public void CanImplicitCastFromDouble(double originalInput)
        {
            ProperFraction ui = originalInput;
            ui.ShouldBe(new ProperFraction(originalInput));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        [InlineData(-200)]
        [InlineData(200)]
        public void CanRejectInvalidInputs(double invalidInput)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new ProperFraction(invalidInput));
        }
    }
}
