using Faze.Abstractions.Core;
using System;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class UnitIntervalTests
    {
        [Fact]
        public void AcceptsValidInput()
        {
            var ui = new UnitInterval(0.5);
        }
    }
}
