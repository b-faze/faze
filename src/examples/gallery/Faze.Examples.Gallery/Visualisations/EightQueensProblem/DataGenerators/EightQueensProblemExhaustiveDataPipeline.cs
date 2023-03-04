using Faze.Core.Adapters;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services.Aggregates;
using Faze.Examples.Gallery.Services.TreeMappers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faze.Core.Extensions;
using Faze.Examples.Games.GridGames;
using Faze.Examples.Games.GridGames.Pieces;
using Faze.Core.TreeMappers;
using Faze.Core.TreeLinq;
using Faze.Core.Data;
using Faze.Core.IO;
using Faze.Examples.Gallery.Services.Serialisers;
using Faze.Examples.Gallery.Services;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem.DataGenerators
{
    public class EightQueensProblemExhaustiveDataPipeline : IDataGenerator
    {
        private const int BoardSize = 8;
        private const int MaxEvaluationDepth = 3;
        public const string Id = "EightQueensProblemSolutions_exhaustive";
        private readonly IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;

        public EightQueensProblemExhaustiveDataPipeline(IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressTracker progress)
        {
            progress.SetMessage(Id);

            CreatePipeline().Run(progress); 

            return Task.CompletedTask;
        }

        private IPipeline CreatePipeline()
        {
            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(Id, treeDataProvider)
                .Map(new EightQueensProblemSolutionTreeMapper(MaxEvaluationDepth))
                .GameTree(new SquareTreeAdapter(BoardSize))
                .Build(() => new PiecesBoardState(new PiecesBoardStateConfig(BoardSize, new QueenPiece(), onlySafeMoves: true)));

            return pipeline;
        }
    }
}
