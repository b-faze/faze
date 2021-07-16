using Faze.Engine.Players;
using System;
using Shouldly;
using Xunit;

namespace Faze.Engine.Tests.Players
{
    public class MoveDistributionTests
    {
        [Fact]
        public void ThrowsWhenGetMoveOfEmptyList()
        {
            var moves = new MoveDistribution<int>(new (int, uint)[0]);
            Should.Throw<Exception>(() =>
            {
                moves.GetMove(1);
            });
        }

        [Fact]
        public void ThrowsWhenAllZeroConfidence()
        {
            Should.Throw<Exception>(() =>
            {
                var moves = new MoveDistribution<int>(new (int, uint)[] { (0, 0), (1, 0), (2, 0) });
            });
        }

        [Fact]
        public void CanGetMoveForUniformConfidence()
        {
            const uint confidence = 1;
            var moves = new MoveDistribution<int>(new (int, uint)[] { (0, confidence), (1, confidence), (2, confidence), (3, confidence) });

            moves.GetMove(0).ShouldBe(0);
            moves.GetMove(0.24).ShouldBe(0);
            moves.GetMove(0.25).ShouldBe(1);
            moves.GetMove(0.26).ShouldBe(1);
            moves.GetMove(0.5).ShouldBe(2);
            moves.GetMove(0.74).ShouldBe(2);
            moves.GetMove(0.75).ShouldBe(3);
            moves.GetMove(0.99).ShouldBe(3);
        }

        [Fact]
        public void CanExcludeZeroConfidence()
        {
            const uint confidence = 1;
            var moves = new MoveDistribution<int>(new (int, uint)[] { (0, 0), (1, confidence), (2, confidence), (3, 0) });

            moves.GetMove(0).ShouldBe(1);
            moves.GetMove(0.24).ShouldBe(1);
            moves.GetMove(0.25).ShouldBe(1);
            moves.GetMove(0.26).ShouldBe(1);
            moves.GetMove(0.5).ShouldBe(2);
            moves.GetMove(0.75).ShouldBe(2);
            moves.GetMove(0.99).ShouldBe(2);
        }
    }
}
