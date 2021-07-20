using Faze.Core.Serialisers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Faze.Core.Tests.SerialiserTests
{
    public class NullableIntSerialiserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void CanWriteAndRead(int? originalValue)
        {
            var serialiser = new NullableIntSerialiser();

            var actualValue = serialiser.Deserialize(serialiser.Serialize(originalValue));

            actualValue.ShouldBe(originalValue);

        }
    }
}
