using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.OX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.OX.DataGenerators
{
    public class OXDataGenerator5 : IDataGenerator
    {
        public const string Id = "OX_depth5_sim100";
        private readonly IGalleryService galleryService;
        private readonly ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OXDataGenerator5(IGalleryService galleryService, ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public Task Generate()
        {
            var filename = galleryService.GetDataFilename(Id);
            OXDataGenerator.GetWritePipeline(filename, treeDataProvider, 100, 5).Run();

            return Task.CompletedTask;
        }


    }

    public static class OXDataGenerator
    {
        public static IPipeline GetWritePipeline(string filename, ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider, int simulations, int depth)
        {
            ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, WinLoseDrawResultAggregate> resultsMapper = new WinLoseDrawResultsTreeMapper(new GameSimulator(), simulations);

            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(filename, treeDataProvider)
                .Map(resultsMapper)
                .LimitDepth(depth)
                .GameTree(new OXStateTreeAdapter())
                .Build(() => OXState.Initial);

            return pipeline;
        }
    }
}
