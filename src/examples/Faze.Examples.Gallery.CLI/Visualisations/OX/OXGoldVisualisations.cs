using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.Gallery.CLI.Visualisations.OX.DataGenerators;
using Faze.Rendering.TreeRenderers;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.OX
{
    public class OXGoldVisualisations : IImageGenerator
    {
        private readonly OXGoldImagePipeline pipelineProvider;

        public OXGoldVisualisations(OXGoldImagePipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        public Task Generate(IProgressBar progress)
        {
            var maxDepth = 6;
            progress.SetMaxTicks(maxDepth);

            for (var i = 1; i < maxDepth; i++)
            {
                Run(progress, i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressBar progress, int depth)
        {
            var id = $"OXGold{depth}";
            progress.SetMessage(id);

            var metaData = new GalleryItemMetadata
            {
                Id = id,
                FileName = $"OX Gold {depth}.png",
                Albums = new[] { "OX" },
                Description = "Desc...",
            };

            var rendererConfig = new SquareTreeRendererOptions(3, 500)
            {
                MaxDepth = depth
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, x => x.GetWinsOverLoses());
            pipeline.Run(progress);

            return Task.CompletedTask;
        }
    }
}
