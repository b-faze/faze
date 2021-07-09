using CommandLine;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.IO;
using Faze.Core.Pipelines;
using Faze.Core.Serialisers;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.CLI.Commands;
using Faze.Examples.Gallery.CLI.Extensions;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.Gallery.CLI.Utilities;
using Faze.Examples.Gallery.CLI.Visualisations.OX;
using Faze.Examples.Gallery.CLI.Visualisations.OX.DataGenerators;
using Faze.Examples.Gallery.CLI.Visualisations.PieceBoards;
using Faze.Examples.OX;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI
{
    class Program
    {
        static async Task<int> Main(string[] args)
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
                .AddSingleton<OXSimulatedDataPipeline>()
                .AddSingleton<OXGoldImagePipeline>()
                .AddSingleton<PieceBoardImagePipeline>()
                .AddSingletons<IDataGenerator>(Assembly.GetAssembly(typeof(Program)))
                .AddSingletons<IImageGenerator>(Assembly.GetAssembly(typeof(Program)))
                .AddMediatR(Assembly.GetAssembly(typeof(Program)))
                .BuildServiceProvider();

            var x = serviceProvider.GetService<IGalleryService>();
            var mediator = serviceProvider.GetService<IMediator>();

            return await Parser.Default.ParseArguments<GenerateDataCommand, GenerateImagesCommand>(args)
                    .MapResult(
                        (GenerateDataCommand o) => mediator.Send(o), 
                        (GenerateImagesCommand o) => mediator.Send(o), 
                        (CheckImagesCommand o) => mediator.Send(o), 
                        err => Task.FromResult(1));
        }
    }
}
