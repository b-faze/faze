using Faze.Abstractions.Players;
using Faze.Engine.Players;

namespace Faze.Engine.Simulators
{
    public class MonkeyAgentProvider : IAgentProvider
    {
        public IPlayer GetPlayer(PlayerIndex playerIndex)
        {
            return new MonkeyAgent();
        }
    }
}
