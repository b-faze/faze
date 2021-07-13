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
using Faze.Examples.Gallery.Visualisations.OX;
using Faze.Examples.Gallery.Visualisations.OX.DataGenerators;
using Faze.Examples.Gallery.Visualisations.PieceBoards;
using Faze.Examples.Gallery.Visualisations.PieceBoards.DataGenerators;
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
            var serviceProvider = new ServiceCollection()
                .AddSingleton(new GalleryServiceConfig
                {
                    ImageBasePath = @"..\..\..\output\gallery",
                    DataBasePath = @"..\..\..\output\data"
                })
                .AddSingleton<IProgressManager, GalleryProgressManager>()
                .AddSingleton<IGalleryService, GalleryService>()

                .AddSingleton<ITreeDataProvider<WinLoseDrawResultAggregate>, GalleryTreeDataProvider<WinLoseDrawResultAggregate>>()
                .AddSingleton<IValueSerialiser<WinLoseDrawResultAggregate>, WinLoseDrawResultAggregateSerialiser>()
                .AddSingleton<ITreeSerialiser<WinLoseDrawResultAggregate>, JsonTreeSerialiser<WinLoseDrawResultAggregate>>()

                .AddSingleton<ITreeDataProvider<EightQueensProblemSolutionAggregate>, GalleryTreeDataProvider<EightQueensProblemSolutionAggregate>>()
                .AddSingleton<IValueSerialiser<EightQueensProblemSolutionAggregate>, EightQueensProblemSolutionAggregateSerialiser>()
                .AddSingleton<ITreeSerialiser<EightQueensProblemSolutionAggregate>, JsonTreeSerialiser<EightQueensProblemSolutionAggregate>>()

                .AddSingleton<OXSimulatedDataPipeline>()
                .AddSingleton<OXGoldImagePipeline>()
                .AddSingleton<PieceBoardImagePipeline>()
                .AddSingletons<IDataGenerator>(Assembly.GetAssembly(typeof(Program)))
                .AddSingletons<IImageGenerator>(Assembly.GetAssembly(typeof(Program)))
                .AddMediatR(Assembly.GetAssembly(typeof(Program)))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
