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
    public class ColorSerialiserTests
    {
        [Fact]
        public void CanWriteAndRead()
        {
            var colors = new[]
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.White,
                Color.Black,
                Color.Aqua
            };
            var serialiser = new ColorSerialiser();


            foreach (var originalColor in colors)
            {
                var actualColor = serialiser.Deserialize(serialiser.Serialize(originalColor));

                actualColor.A.ShouldBe(originalColor.A);
                actualColor.R.ShouldBe(originalColor.R);
                actualColor.G.ShouldBe(originalColor.G);
                actualColor.B.ShouldBe(originalColor.B);
            }

        }
    }
}
