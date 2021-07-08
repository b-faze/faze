using Faze.Abstractions.Core;
using Faze.Abstractions.Engine;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System;

namespace Faze.Core
{
    public class GameVisualisationSettings<TMove, TResult, TResultAgg>
    {
        public IGameStateFactory<TMove, TResult> GameFactory { get; }
        public IGameStateTreeAdapter<TMove> StateTreeAdapter { get; }
        public ITreeMapper<TResult, TResultAgg> Engine { get; }
        public ITreePainter<TResultAgg> Painter { get; }
        public IPaintedTreeRenderer Renderer { get; }
    }

    public class GameVisualisation<TMove, TResult, TResultAgg>
    {
        private readonly GameVisualisationSettings<TMove, TResult, TResultAgg> settings;

        public GameVisualisation(GameVisualisationSettings<TMove, TResult, TResultAgg> settings) 
        {
            this.settings = settings;
        }

        public void Run()
        {
            var state = settings.GameFactory.Create();
            var stateTreeAdapter = settings.StateTreeAdapter;
            var engine = settings.Engine;
            var painter = settings.Painter;
            var renderer = settings.Renderer;

            var visibleStateTree = renderer.GetVisible(state.ToStateTree(stateTreeAdapter));
            //var resultsTree = engine.GetResultsTree(visibleStateTree);
            //var renderTree = painter.Paint(resultsTree);

            //renderer.Draw(renderTree);
        }
    }
}
