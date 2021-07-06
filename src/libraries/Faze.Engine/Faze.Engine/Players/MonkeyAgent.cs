using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Engine.Players
{
    public class MonkeyAgent : IPlayer
    {
        private readonly Random rnd;

        public MonkeyAgent(Random rnd = null)
        {
            this.rnd = rnd ?? ThreadSafeRandom.Random();
        }

        public TMove ChooseMove<TMove, TResult, TPlayer>(IGameState<TMove, TResult, TPlayer> state)
        {
            var availableMoves = state.AvailableMoves;
            var rndMove = rnd.Next(0, availableMoves.Length);
            var move = availableMoves[rndMove];
            return move;
        }
    }
}
