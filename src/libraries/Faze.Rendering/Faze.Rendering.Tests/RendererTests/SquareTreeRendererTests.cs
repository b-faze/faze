using Faze.Abstractions.Rendering;
using Faze.Rendering.TreeRenderers;
using Faze.Rendering.Tests.Utilities;
using System.Drawing.Imaging;
using Xunit;
using Faze.Core.Pipelines;
using Faze.Utilities.Testing;
using Faze.Utilities.Testing.Extensions;
using Faze.Core.Extensions;
using System.Drawing;
using Faze.Core.Serialisers;
using Faze.Rendering.TreePainters;
using Faze.Core;
using Faze.Core.Data;
using System.Reflection;

namespace Faze.Rendering.Tests.RendererTests
{
    public class SquareTreeRendererTests
    {
        private readonly TestImageRegressionService testImageRegressionService;
        private readonly TestFileTreeDataProvider<Color> testFileTreeDataProvider;
        private readonly TestEmbededResourceTreeDataReader<Color> testEmbededResourceTreeDataReader;
        public SquareTreeRendererTests()
        {
            var resourcePath = @"../../../Resources/SquareTreeRenderer";
            var resourceNamespace = "Faze.Rendering.Tests.Resources.SquareTreeRenderer";
            var config = new TestImageRegressionServiceConfig
            {
                ExpectedImageDirectory = resourcePath
            };
            this.testImageRegressionService = new TestImageRegressionService(config);
            this.testFileTreeDataProvider = new TestFileTreeDataProvider<Color>(resourcePath, new ColorSerialiser());
            this.testEmbededResourceTreeDataReader = new TestEmbededResourceTreeDataReader<Color>(Assembly.GetExecutingAssembly(), resourceNamespace, new ColorSerialiser());
        }

        [Theory]
        [InlineData(3, 500, 1, 0, "static_3_500_1_0")]
        public void CompareStaticTestCases(int squareSize, int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize, imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SquareTreeRenderer(rendererOptions);

            testImageRegressionService.TestImageDiffPipeline($"{id}.png", $"{id}.diff.png")
                .Render(renderer)
                .LoadTree($"{id}.json", testEmbededResourceTreeDataReader)
                .Run();
        }

        [Theory]
        [InlineData(3, 500, 1, 0, "static_3_500_1_0", Skip = "manual only")]
        public void GenerateStaticTestCases(int squareSize, int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize, imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SquareTreeRenderer(rendererOptions);

            testImageRegressionService.GenerateTestCasePipeline($"{id}.png")
                .Render(renderer)
                .LoadTree($"{id}.json", testFileTreeDataProvider)
                .Run();
        }

        [Theory]
        [InlineData(1, 500, 1, 0, "dynamic_1_500_1_0")]
        [InlineData(2, 500, 1, 0, "dynamic_2_500_1_0")]
        [InlineData(2, 500, 2, 0, "dynamic_2_500_2_0")]
        [InlineData(2, 500, 3, 0, "dynamic_2_500_3_0")]
        [InlineData(2, 500, 3, 0.1, "dynamic_2_500_3_0.1")]
        [InlineData(3, 500, 1, 0, "dynamic_3_500_1_0")]
        [InlineData(3, 500, 2, 0, "dynamic_3_500_2_0")]
        [InlineData(3, 500, 3, 0, "dynamic_3_500_3_0")]
        [InlineData(3, 500, 3, 0.1, "dynamic_3_500_3_0.1")]
        public void CompareDynamicTestCases(int squareSize, int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize, imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SquareTreeRenderer(rendererOptions);
            var dynamicDataProvider = new DynamicTreeDataProvider<object>();
            var dynamicDataOptions = new DynamicSquareTreeOptions<object>(squareSize, depth, info => null);

            testImageRegressionService.TestImageDiffPipeline($"{id}.png", $"{id}.diff.png")
                .Render(renderer)
                .Paint<object>(new CheckeredTreePainter())
                .LoadTree(dynamicDataOptions, dynamicDataProvider)
                .Run();
        }

        [Theory]
        [InlineData(1, 500, 1, 0, "dynamic_1_500_1_0", Skip = "manual only")]
        [InlineData(2, 500, 1, 0, "dynamic_2_500_1_0", Skip = "manual only")]
        [InlineData(2, 500, 2, 0, "dynamic_2_500_2_0", Skip = "manual only")]
        [InlineData(2, 500, 3, 0, "dynamic_2_500_3_0", Skip = "manual only")]
        [InlineData(2, 500, 3, 0.1, "dynamic_2_500_3_0.1", Skip = "manual only")]
        [InlineData(3, 500, 1, 0, "dynamic_3_500_1_0", Skip = "manual only")]
        [InlineData(3, 500, 2, 0, "dynamic_3_500_2_0", Skip = "manual only")]
        [InlineData(3, 500, 3, 0, "dynamic_3_500_3_0", Skip = "manual only")]
        [InlineData(3, 500, 3, 0.1, "dynamic_3_500_3_0.1", Skip = "manual only")]
        public void GenerateDynamicTestCases(int squareSize, int imgSize, int depth, float borderProportion, string id)
        {
            var rendererOptions = new SquareTreeRendererOptions(squareSize, imgSize)
            {
                BorderProportion = borderProportion,
                MaxDepth = depth
            };

            var renderer = new SquareTreeRenderer(rendererOptions);
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

