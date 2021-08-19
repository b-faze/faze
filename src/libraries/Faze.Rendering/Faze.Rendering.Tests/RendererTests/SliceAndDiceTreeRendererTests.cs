using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core;
using Faze.Core.Serialisers;
using Faze.Rendering.Tests.Utilities;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using Faze.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using Xunit;
using Faze.Utilities.Testing.Extensions;
using Faze.Core.Extensions;
using Faze.Core.Data;

namespace Faze.Rendering.Tests.RendererTests
{
    public class SliceAndDiceTreeRendererTests
    {
        private readonly TestImageRegressionService testImageRegressionService;
        private readonly TestFileTreeDataProvider<Color> testFileTreeDataProvider;
        public SliceAndDiceTreeRendererTests()
        {
            var resourcePath = @"../../../Resources/SliceAndDiceTreeRenderer";
            var config = new TestImageRegressionServiceConfig
            {
                ExpectedImageDirectory = resourcePath
            };
            this.testImageRegressionService = new TestImageRegressionService(config);
            this.testFileTreeDataProvider = new TestFileTreeDataProvider<Color>(resourcePath, new ColorSerialiser());
        }

        [Theory]
        [InlineData(500, 1, 0, "static1_500_1_0")]
        [InlineData(500, 2, 0, "static2_500_2_0")]
        public void CompareStaticTestCases(int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SliceAndDiceTreeRendererOptions(imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SliceAndDiceTreeRenderer(rendererOptions);

            testImageRegressionService.TestImageDiffPipeline($"{id}.png", $"{id}.diff.png")
                .Render(renderer)
                .LoadTree($"{id}.json", testFileTreeDataProvider)
                .Run();
        }

        [Theory]
        [InlineData(500, 1, 0, "static1_500_1_0", Skip = "manual only")]
        [InlineData(500, 2, 0, "static2_500_2_0", Skip = "manual only")]
        public void GenerateStaticTestCases(int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SliceAndDiceTreeRendererOptions(imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SliceAndDiceTreeRenderer(rendererOptions);

            testImageRegressionService.GenerateTestCasePipeline($"{id}.png")
                .Render(renderer)
                .LoadTree($"{id}.json", testFileTreeDataProvider)
                .Run();
        }

        [Theory]
        [InlineData(1, 500, 1, 0, "dynamic_square_1_500_1_0")]
        [InlineData(2, 500, 1, 0, "dynamic_square_2_500_1_0")]
        [InlineData(2, 500, 2, 0, "dynamic_square_2_500_2_0")]
        [InlineData(2, 500, 3, 0, "dynamic_square_2_500_3_0")]
        [InlineData(2, 500, 3, 0.1, "dynamic_square_2_500_3_0.1")]
        [InlineData(3, 500, 1, 0, "dynamic_square_3_500_1_0")]
        [InlineData(3, 500, 2, 0, "dynamic_square_3_500_2_0")]
        [InlineData(3, 500, 3, 0, "dynamic_square_3_500_3_0")]
        [InlineData(3, 500, 3, 0.1, "dynamic_square_3_500_3_0.1")]
        public void CompareDynamicTestCases(int squareSize, int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SliceAndDiceTreeRendererOptions(imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SliceAndDiceTreeRenderer(rendererOptions);
            var dynamicDataProvider = new DynamicTreeDataProvider<object>();
            var dynamicDataOptions = new DynamicSquareTreeOptions<object>(squareSize, depth, info => null);

            testImageRegressionService.TestImageDiffPipeline($"{id}.png", $"{id}.diff.png")
                .Render(renderer)
                .Paint<object>(new CheckeredTreePainter())
                .LoadTree(dynamicDataOptions, dynamicDataProvider)
                .Run();
        }

        [Theory]
        [InlineData(1, 500, 1, 0, "dynamic_square_1_500_1_0", Skip = "manual only")]
        [InlineData(2, 500, 1, 0, "dynamic_square_2_500_1_0", Skip = "manual only")]
        [InlineData(2, 500, 2, 0, "dynamic_square_2_500_2_0", Skip = "manual only")]
        [InlineData(2, 500, 3, 0, "dynamic_square_2_500_3_0", Skip = "manual only")]
        [InlineData(2, 500, 3, 0.1, "dynamic_square_2_500_3_0.1", Skip = "manual only")]
        [InlineData(3, 500, 1, 0, "dynamic_square_3_500_1_0", Skip = "manual only")]
        [InlineData(3, 500, 2, 0, "dynamic_square_3_500_2_0", Skip = "manual only")]
        [InlineData(3, 500, 3, 0, "dynamic_square_3_500_3_0", Skip = "manual only")]
        [InlineData(3, 500, 3, 0.1, "dynamic_square_3_500_3_0.1", Skip = "manual only")]
        public void GenerateDynamicTestCases(int squareSize, int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SliceAndDiceTreeRendererOptions(imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SliceAndDiceTreeRenderer(rendererOptions);
            var dynamicDataProvider = new DynamicTreeDataProvider<object>();
            var dynamicDataOptions = new DynamicSquareTreeOptions<object>(squareSize, depth, info => null);

            testImageRegressionService.GenerateTestCasePipeline($"{id}.png")
                .Render(renderer)
                .Paint<object>(new CheckeredTreePainter())
                .LoadTree(dynamicDataOptions, dynamicDataProvider)
                .Run();
        }
    }
}
