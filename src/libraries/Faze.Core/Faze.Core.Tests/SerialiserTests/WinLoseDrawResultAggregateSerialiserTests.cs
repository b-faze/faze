using Faze.Core.Serialisers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Faze.Abstractions.GameResults;

namespace Faze.Core.Tests.SerialiserTests
{
    public class WinLoseDrawResultAggregateSerialiserTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        public void CanWriteAndRead(uint wins, uint loses, uint draws)
        {
            var serialiser = new WinLoseDrawResultAggregateSerialiser();
            var originalValue = new WinLoseDrawResultAggregate(wins, loses, draws);

            var actualValue = serialiser.Deserialize(serialiser.Serialize(originalValue));

            actualValue.ShouldBe(originalValue);

        }
    }
}
