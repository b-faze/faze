using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Shouldly;
using System;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Faze.Rendering.Tests.ColorInterpolatorTests
{
    public class GoldInterpolatorTests
    {
        [Fact]
        public void HasCorrectValuesForBlueToRed()
        {
            var colorInterpolator = GetColorInterpolator();
            var values = Enumerable.Range(0, 255).Select(i => (byte)i);

            foreach (var value in values)
            {
                var input = (double)value / 255;
                var color = colorInterpolator.GetColor(input);

                var drawOffset = 1 - 2 * Math.Abs(0.5 - input);
                var expectedR = (byte)(255 * input + drawOffset * 127);
                var expectedG = (byte)(drawOffset * 255);
                var expectedB = (byte)(255 * (1 - input) - drawOffset * 127);

                color.A.ShouldBe((byte)255);
                color.R.ShouldBe(expectedR);
                color.G.ShouldBe(expectedG);
                color.B.ShouldBe(expectedB);
            }
        }

        private IColorInterpolator GetColorInterpolator()
        {
            return new GoldInterpolator();
        }
    }
}
