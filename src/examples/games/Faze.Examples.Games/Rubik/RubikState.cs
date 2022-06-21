using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Examples.Games.Rubik
{
    public class RubikState : IGameState<GridMove, RubikResult?>
    {
        public static RubikState InitialSolved => new RubikState();

        private RubikState() { }

        public PlayerIndex CurrentPlayerIndex => PlayerIndex.P1;

        public IEnumerable<GridMove> GetAvailableMoves()
        {
            throw new NotImplementedException();
        }

        public RubikResult? GetResult()
        {
            return null;
        }

        public IGameState<GridMove, RubikResult?> Move(GridMove move)
        {
            throw new NotImplementedException();
        }
    }
}
