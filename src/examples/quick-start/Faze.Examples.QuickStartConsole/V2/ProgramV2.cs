using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using System;
using Faze.Core.TreeLinq;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;

namespace Faze.Examples.QuickStartConsole.V2
{
    class ProgramV2
    {
        public static void Run(string[] args)
        {
            var size = 3;
            var maxDepth = 4;

            var rendererOptions = new SquareTreeRendererOptions(size, 500)
            {
                BorderProportion = 0.1f,
                MaxDepth = 4
            };

            IPipeline pipeline = ReversePipelineBuilder.Create()
                .File("my_first_visualisation.png")
                .Render(new SquareTreeRenderer(rendererOptions))
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(agg => agg.GetWinsOverLoses()))
                .Map(new MyCustomGameResultsMapper())
                .GameTree()
                .Build(() => MyCustomGame.Initial(15));

            pipeline.Run();
        }
    }
}
