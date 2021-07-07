using Faze.Abstractions.Rendering;
using Faze.Rendering.TreeRenderers;
using Faze.Rendering.Tests.Utilities;
using System.Drawing.Imaging;
using Xunit;
using Faze.Rendering.Extensions;

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
        public void Test1(int squareSize, int depth, float borderProportion)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize)
            {
                BorderProportions = borderProportion
            };
            var renderer = new SquareTreeRenderer(rendererOptions, 600);
            var tree = TreeUtilities.CreateGreyPaintedSquareTree(squareSize, depth);
            var filename = FileUtilities.GetTestOutputPath(nameof(SquareTreeRendererTests), 
                $"Test1_{squareSize}_{depth}_{borderProportion}.png");
            
            renderer.Draw(tree);
            renderer.Save(filename);

        }        
        
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
        public void RainbowTests(int squareSize, int depth, float borderProportion)
        {
            var testName = nameof(RainbowTests);
            var rendererOptions = new SquareTreeRendererOptions(squareSize)
            {
                BorderProportions = borderProportion
            };
            var renderer = new SquareTreeRenderer(rendererOptions, 600);
            var tree = TreeUtilities.CreateRainbowPaintedSquareTree(squareSize, depth);
            var filename = FileUtilities.GetTestOutputPath(nameof(SquareTreeRendererTests), 
                $"{testName}_{squareSize}_{depth}_{borderProportion}.png");

            renderer.Draw(tree);
            renderer.Save(filename);

        }

        [Theory]
        [InlineData(2, 3)]
        public void RainbowBorderTests(int squareSize, int depth)
        {
            float minBorder = 0;
            float maxBorder = 0.2f;
            var steps = 10;
            for (var i = 0; i < steps; i++)
            {
                float borderProportion = minBorder + (maxBorder - minBorder) * ((float)i / steps);
                var testName = nameof(RainbowBorderTests);
                var rendererOptions = new SquareTreeRendererOptions(squareSize)
                {
                    BorderProportions = borderProportion
                };
                var renderer = new SquareTreeRenderer(rendererOptions, 600);
                var tree = TreeUtilities.CreateRainbowPaintedSquareTree(squareSize, depth);
                var filename = FileUtilities.GetTestOutputPath(nameof(SquareTreeRendererTests),
                    $"{testName}_{squareSize}_{depth}_{i}.png");

                renderer.Draw(tree);
                renderer.Save(filename);
            }


        }
    }
}

