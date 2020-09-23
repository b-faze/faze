using Faze.Abstractions.Rendering;
using Faze.Rendering.TreeRenderers;
using Faze.Rendering.Tests.Utilities;
using System.Drawing.Imaging;
using Xunit;

namespace Faze.Rendering.Tests.RendererTests
{
    public class SquareTreeRendererTests
    {
        [Theory]
        [InlineData(1, 0, 0.1)]
        [InlineData(1, 1, 0.1)]
        [InlineData(1, 2, 0.1)]
        [InlineData(1, 3, 0.1)]
        [InlineData(1, 4, 0.1)]
        [InlineData(2, 0, 0.1)]
        [InlineData(2, 1, 0.1)]
        [InlineData(2, 2, 0.1)]
        [InlineData(2, 3, 0.1)]
        [InlineData(2, 4, 0.1)]
        [InlineData(3, 0, 0.1)]
        [InlineData(3, 1, 0.1)]
        [InlineData(3, 2, 0.1)]
        [InlineData(3, 3, 0.1)]
        [InlineData(3, 4, 0.1)]
        public void Test1(int squareSize, int depth, double borderProportion)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize)
            {
                BorderProportions = borderProportion
            };
            var renderer = new SquareTreeRenderer(rendererOptions);
            var tree = TreeUtilities.CreatePaintedSquareTree(squareSize, depth);
            var filename = FileUtilities.GetTestOutputPath(nameof(SquareTreeRendererTests), 
                $"Test1_{squareSize}_{depth}_{borderProportion}.png");

            using (var img = renderer.Draw(tree, 600))
            {
                img.Save(filename, ImageFormat.Png);
            }
            
        }
    }
}

