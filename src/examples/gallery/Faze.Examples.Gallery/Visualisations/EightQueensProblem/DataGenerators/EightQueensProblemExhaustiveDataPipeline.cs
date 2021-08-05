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
        public const string Id = "EightQueensProblemSolutions_exhaustive";
        private readonly IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;

        public EightQueensProblemExhaustiveDataPipeline(IGalleryService galleryService, IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
            //var treeSerialiser = new LeafTreeSerialiser<EightQueensProblemSolutionAggregate>(new EightQueensProblemSolutionAggregateSerialiser());
            //this.treeDataProvider = new GalleryTreeDataProvider<EightQueensProblemSolutionAggregate>(galleryService, treeSerialiser);
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
                //.Map(resultsMapper)
                //.LimitDepth(3)
                //.Evaluate(3)
                //.Map(new AggregateTreeMapper<EightQueensProblemSolutionAggregate>())
                //.Evaluate()
                //.Map<EightQueensProblemSolutionAggregate, IGameState<GridMove, SingleScoreResult?>>(t =>
                //{
                //    return t.MapValue(v =>
                //    {
                //        var result = v.GetResult();
                //        if (result.HasValue)
                //            return new EightQueensProblemSolutionAggregate(result.Value);

                //        return new EightQueensProblemSolutionAggregate();
                //    });
                //})
                //.Map<EightQueensProblemSolutionAggregate, IGameState<GridMove, SingleScoreResult?>>(t =>
                //{
                //    if (!t.IsLeaf())
                //    {
                //        return new Tree<EightQueensProblemSolutionAggregate>(new EightQueensProblemSolutionAggregate());
                //    }

                //    var gameResults = new MoveOrderInvariantTreeMapper2().Map(t.Value.ToStateTree(), NullProgressTracker.Instance)
                //        .GetLeaves()
                //        .Select(l => l.Value.GetResult());

                //    var aggResult = new EightQueensProblemSolutionAggregate();
                //    foreach (var result in gameResults)
                //    {
                //        if (result == null)
                //            continue;

                //        aggResult.Add(result.Value);
                //    }

                //    return new Tree<EightQueensProblemSolutionAggregate>(aggResult);
                //})
                .Map(new EightQueensProblemSolutionTreeMapper(3))
                //.Map<EightQueensProblemSolutionAggregate, IGameState<GridMove, SingleScoreResult?>>(t =>
                //{
                //    return t.MapValue(v =>
                //    {
                //        var result = v.GetResult();
                //        if (result.HasValue)
                //            return new EightQueensProblemSolutionAggregate(result.Value);

                //        return new EightQueensProblemSolutionAggregate();
                //    });
                //})
                //.Map(new MoveOrderInvariantTreeMapper2())
                .GameTree(new SquareTreeAdapter(BoardSize))
                .Build(() => new PiecesBoardState(new PiecesBoardStateConfig(BoardSize, new QueenPiece(), onlySafeMoves: true)));

            return pipeline;
        }
    }
}
