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
        private readonly OXGoldPipeline pipelineProvider;

        public OXGoldVisualisations(OXGoldPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        public Task Generate()
        {
            var metaData = new GalleryItemMetadata
            {
                Id = "OXGold1",
                FileName = "OX Gold 1.png",
                Albums = new[] { "OX" },
                Description = "Desc...",
            };

            var rendererConfig = new SquareTreeRendererOptions(3, 500)
            {
                MaxDepth = 1
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, x => x.GetWinsOverLoses());
            pipeline.Run();

            return Task.CompletedTask;
        }
    }
}
