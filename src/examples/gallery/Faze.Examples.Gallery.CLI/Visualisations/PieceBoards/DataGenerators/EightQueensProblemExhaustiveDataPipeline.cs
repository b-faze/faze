using Faze.Abstractions.Adapters;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.GridGames;
using Faze.Examples.GridGames.Pieces;
using Faze.Examples.OX;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.PieceBoards.DataGenerators
{
    public class EightQueensProblemExhaustiveDataPipeline : IDataGenerator
    {
        private const int BoardSize = 8;
        public const string Id = "EightQueensProblemSolutions_exhaustive";
        private readonly ITreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;
        private readonly ITreeMapper<IGameState<GridMove, SingleScoreResult?>, EightQueensProblemSolutionAggregate> resultsMapper;

        public EightQueensProblemExhaustiveDataPipeline(ITreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
            this.resultsMapper = new EightQueensProblemSolutionTreeMapper();
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressBar progress)
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
