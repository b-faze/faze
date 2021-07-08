using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.IO;
using Faze.Core.Pipelines;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.CLI.Visualisations.OX;
using Faze.Examples.OX;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using System;
using System.Linq;

namespace Faze.Examples.Gallery.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var galleryService = new GalleryService(new GalleryServiceConfig
            {
                BasePath = @"../../gallery"
            });
            var treeSerialiser = new JsonTreeSerialiser<WinLoseDrawResultAggregate>(new WinLoseDrawResultAggregateSerialiser());
            var filename = "tree.json";

            //GetWritePipeline(filename, treeSerialiser).Run();
            GetReadPipeline(galleryService, filename, treeSerialiser).Run();
        }

        static IPipeline GetWritePipeline(string filename, ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser)
        {
            ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, WinLoseDrawResultAggregate> resultsMapper = new WinLoseDrawResultsTreeMapper(new GameSimulator(), 100);

            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(filename, treeSerialiser)
                .Map(resultsMapper)
                .LimitDepth(2)
                .GameTree(new OXStateTreeAdapter())
                .Build(() => OXState.Initial);

            return pipeline;
        }

        static IPipeline GetReadPipeline(GalleryService galleryService, string filename, ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser)
        {
            var galleryMetaData = new GalleryItemMetadata
            {
                Id = "OXGold1",
                FileName = "OX Gold 1.png",
                Albums = new[] { "OX" },
                Description = "Desc",
            };

            var renderer = new SquareTreeRenderer(new SquareTreeRendererOptions(3, 500)
            {
                MaxDepth = 1
            });
            return new OXPipeline(galleryService, renderer, new GoldInterpolator(), treeSerialiser, filename, galleryMetaData).GetPipeline();
        }
    }

    public class WinLoseDrawResultAggregateSerialiser : IValueSerialiser<WinLoseDrawResultAggregate>
    {
        public WinLoseDrawResultAggregate Deserialize(string valueString)
        {
            var values = valueString.Split(',').Select(uint.Parse).ToArray();
            return new WinLoseDrawResultAggregate(values[0], values[1], values[2]);
        }

        public string Serialize(WinLoseDrawResultAggregate value)
        {
            return $"{value.Wins},{value.Loses},{value.Draws}";
        }
    }
}
