using Faze.Abstractions.GameMoves;
using Xunit;
using Faze.Core.Extensions;
using Shouldly;
using System.Linq;

namespace Faze.Core.Tests.ExtensionTests
{
    public class GridMoveExtensionTests
    {
        [Theory]
        [InlineData(9, 27, 3, 3)]
        [InlineData(9, 28, 3, 3)]
        [InlineData(9, 29, 3, 3)]
        [InlineData(9, 36, 3, 3)]
        [InlineData(9, 37, 3, 3)]
        [InlineData(9, 38, 3, 3)]
        [InlineData(9, 45, 3, 3)]
        [InlineData(9, 46, 3, 3)]
        [InlineData(9, 47, 3, 3)]
        public void ToSubdimension(int dimension, int pos, int newDimension, int expectedPos)
        {
            new GridMove(pos).ToSubdimension(dimension, newDimension).ShouldBe(new GridMove(expectedPos));
        }

        [Fact]
        public void GetBoxes()
        {
            var dimension = 9;
            var subdimension = 3;
            var pos = new GridMove(28);
            var boxes = pos.GetBox(dimension, subdimension);
            boxes.Select(b => (int)b).ShouldBe(new[] { 27, 28, 29, 36, 37, 38, 45, 46, 47 });
        }
    }
}
