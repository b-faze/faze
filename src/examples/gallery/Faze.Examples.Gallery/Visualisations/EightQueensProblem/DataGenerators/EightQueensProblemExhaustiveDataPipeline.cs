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

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem.DataGenerators
{
    public class EightQueensProblemExhaustiveDataPipeline : IDataGenerator
    {
        private const int BoardSize = 8;
        public const string Id = "EightQueensProblemSolutions_exhaustive";
        private readonly IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;
        private readonly ITreeMapper<IGameState<GridMove, SingleScoreResult?>, EightQueensProblemSolutionAggregate> resultsMapper;

        public EightQueensProblemExhaustiveDataPipeline(IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
            this.resultsMapper = new EightQueensProblemSolutionTreeMapper();
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressTracker progress)
        {
            progress.SetMessage(Id);

            CreatePipeline(Id).Run(progress); 

            return Task.CompletedTask;
        }

        private IPipeline CreatePipeline(string dataId)
        {
            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(dataId, treeDataProvider)
                .Map(resultsMapper)
                .LimitDepth(3)
                .GameTree(new SquareTreeAdapter(BoardSize))
                .Build(() => new PiecesBoardState(new PiecesBoardStateConfig(BoardSize, new QueenPiece(), onlySafeMoves: true)));

            return pipeline;
        }
    }
}
