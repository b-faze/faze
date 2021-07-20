using Faze.Abstractions.Rendering;
using Faze.Rendering.ColorInterpolators;
using Shouldly;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Faze.Rendering.Tests.ColorInterpolatorTests
{
    public class LinearInterpolatorTests
    {
        [Fact]
        public void CanHandleSameStartAndEndColor()
        {
            var inputColor = Color.Red;
            var colorInterpolator = GetColorInterpolator(inputColor, inputColor);
            var values = Enumerable.Range(0, 255).Select(i => (byte)i);

            foreach (var value in values)
            {
                var input = (double)value / 255;
                var color = colorInterpolator.GetColor(input);

                color.A.ShouldBe(inputColor.A);
                color.R.ShouldBe(inputColor.R);
                color.G.ShouldBe(inputColor.G);
                color.B.ShouldBe(inputColor.B);
            }
        }

        [Fact]
        public void HasCorrectValuesForBlueToRed()
        {
            var startColor = Color.Blue;
            var endColor = Color.Red;
            var colorInterpolator = GetColorInterpolator(startColor, endColor);
            var values = Enumerable.Range(0, 255).Select(i => (byte)i);

            foreach (var value in values)
            {
                var input = (double)value / 255;
                var color = colorInterpolator.GetColor(input);

                var expectedR = (byte)(startColor.R + (endColor.R - startColor.R) * input);
                var expectedG = (byte)(startColor.G + (endColor.G - startColor.G) * input);
                var expectedB = (byte)(startColor.B + (endColor.B - startColor.B) * input);

                color.A.ShouldBe((byte)255);
                color.R.ShouldBe(expectedR);
                color.G.ShouldBe(expectedG);
                color.B.ShouldBe(expectedB);
            }
        }

        private IColorInterpolator GetColorInterpolator(Color start, Color end)
        {
            return new LinearColorInterpolator(start, end);
        }
    }
}
