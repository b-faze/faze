using Faze.Abstractions.Players;
using Faze.Engine.Players;

namespace Faze.Engine.Simulators
{
    public class MonkeyAgentManager : IAgentManager
    {
        public IPlayer GetPlayer(PlayerIndex playerIndex)
        {
            return new MonkeyAgent();
        }
    }
}
