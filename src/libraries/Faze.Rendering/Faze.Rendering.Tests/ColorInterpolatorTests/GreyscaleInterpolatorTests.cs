using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Shouldly;
using System.Linq;
using Xunit;

namespace Faze.Rendering.Tests.ColorInterpolatorTests
{
    public class GreyscaleInterpolatorTests
    {

        [Fact]
        public void HasCorrectValue()
        {
            var colorInterpolator = GetColorInterpolator();
            var values = Enumerable.Range(0, 255).Select(i => (byte)i);

            foreach (var value in values)
            {
                var input = (double)value / 255;
                var color = colorInterpolator.GetColor(input);

                byte expectedValue = value;
                color.R.ShouldBe(expectedValue);
                color.G.ShouldBe(expectedValue);
                color.B.ShouldBe(expectedValue);
            }
        }


        [Fact]
        public void HasCorrectReverseValue()
        {
            var colorInterpolator = GetColorInterpolator(reverse: true);
            var values = Enumerable.Range(0, 255).Select(i => (byte)i);

            foreach (var value in values)
            {
                var input = (double)value / 255;
                var color = colorInterpolator.GetColor(input);

                byte expectedValue = (byte)(byte.MaxValue - value);
                color.R.ShouldBe(expectedValue);
                color.G.ShouldBe(expectedValue);
                color.B.ShouldBe(expectedValue);
            }
        }

        private IColorInterpolator GetColorInterpolator(bool reverse = false)
        {
            return new GreyscaleInterpolator(reverse);
        }
    }
}
