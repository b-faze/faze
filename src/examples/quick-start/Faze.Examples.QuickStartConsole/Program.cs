using Faze.Abstractions.Core;
using Faze.Core;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using System;

namespace Faze.Examples.QuickStartConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 3;
            var maxDepth = 4;

            var rendererOptions = new SquareTreeRendererOptions(size, 500)
            {
                BorderProportion = 0.1f
            };

            IPipeline pipeline = ReversePipelineBuilder.Create()
                .File("my_first_visualisation.png")
                .Render(new SquareTreeRenderer(rendererOptions))
                .Paint<object>(new CheckeredTreePainter())
                .LoadTree(new DynamicSquareTreeOptions<object>(size, maxDepth, info => null), new DynamicTreeDataProvider<object>());

            pipeline.Run();
        }
    }
}
