using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.IO;
using Faze.Core.Serialisers;
using Faze.Examples.Gallery.CLI.Extensions;
using Faze.Examples.Gallery.CLI.Visualisations.PieceBoards;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Gallery.Services.Aggregates;
using Faze.Examples.Gallery.Services.Serialisers;
using Faze.Examples.Gallery.Visualisations.EightQueensProblem;
using Faze.Examples.Gallery.Visualisations.OX;
using Faze.Examples.Gallery.Visualisations.OX.DataGenerators;
using Faze.Examples.Gallery.Visualisations.OX3D;
using Faze.Examples.Gallery.Visualisations.OX3D.DataGenerators;
using Faze.Examples.Gallery.Visualisations.PieceBoards;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Faze.Examples.Gallery.CLI.Utilities
{
    public static class ServiceProviderBuilder
    {
        public static IServiceProvider Build()
        {
            var programAssembly = Assembly.GetAssembly(typeof(Program));
            var galleryAssembly = Assembly.GetAssembly(typeof(IDataGenerator));

            var serviceProvider = new ServiceCollection()
                .AddSingleton(new GalleryServiceConfig
                {
                    ImageBasePath = @"..\..\..\output\gallery",
                    DataBasePath = @"..\..\..\output\data"
                })
                .AddSingleton<IProgressManager, GalleryProgressManager>()
                .AddSingleton<IGalleryService, GalleryService>()

                .AddSingleton<IFileTreeDataProvider<WinLoseDrawResultAggregate>, GalleryTreeDataProvider<WinLoseDrawResultAggregate>>()
                .AddSingleton<IValueSerialiser<WinLoseDrawResultAggregate>, WinLoseDrawResultAggregateSerialiser>()
                .AddSingleton<ITreeSerialiser<WinLoseDrawResultAggregate>, JsonTreeSerialiser<WinLoseDrawResultAggregate>>()

                .AddSingleton<IPipelineProvider, PipelineProvider>()
                .AddSingleton<IFileTreeDataProvider<EightQueensProblemSolutionAggregate>, GalleryTreeDataProvider<EightQueensProblemSolutionAggregate>>()
                .AddSingleton<IValueSerialiser<EightQueensProblemSolutionAggregate>, EightQueensProblemSolutionAggregateSerialiser>()
                .AddSingleton<ITreeSerialiser<EightQueensProblemSolutionAggregate>, JsonTreeSerialiser<EightQueensProblemSolutionAggregate>>()

                .AddSingleton<OXSimulatedDataPipeline>()
                .AddSingleton<OX3DSimulatedDataPipeline>()
                .AddSingleton<OXGoldVideoPipeline>()
                .AddSingleton<OXGoldImagePipeline>()
                .AddSingleton<OX3DGoldImagePipeline>()
                .AddSingleton<PieceBoardImagePipeline>()

                .AddSingletons<IDataGenerator>(galleryAssembly)
                .AddSingletons<IImageGenerator2>(galleryAssembly)
                .AddSingletons<IVisualisationPipeline>(galleryAssembly)

                .AddMediatR(programAssembly)
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
