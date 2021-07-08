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
using Faze.Examples.Gallery.CLI.Visualisations.OX;
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
                .AddSingleton<IGalleryService, GalleryService>()
                .AddSingleton<IValueSerialiser<WinLoseDrawResultAggregate>, WinLoseDrawResultAggregateSerialiser>()
                .AddSingleton<ITreeSerialiser<WinLoseDrawResultAggregate>, JsonTreeSerialiser<WinLoseDrawResultAggregate>>()
                .AddSingletons<IDataGenerator>(Assembly.GetAssembly(typeof(Program)))
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

        //static IPipeline GetReadPipeline(GalleryService galleryService, string filename, ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser)
        //{
        //    var galleryMetaData = new GalleryItemMetadata
        //    {
        //        Id = "OXGold1",
        //        FileName = "OX Gold 1.png",
        //        Albums = new[] { "OX" },
        //        Description = "Desc",
        //    };

        //    var renderer = new SquareTreeRenderer(new SquareTreeRendererOptions(3, 500)
        //    {
        //        MaxDepth = 1
        //    });
        //    return new OXPipeline(galleryService, renderer, new GoldInterpolator(), treeSerialiser, filename, galleryMetaData).GetPipeline();
        //}
    }
}
