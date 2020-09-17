namespace Faze.Abstractions.GameResults
{
    public class WinLoseResult<TPlayer>
    {
        private WinLoseResult(TPlayer winningPlayer)
        {
            WinningPlayer = winningPlayer;
        }

        public static WinLoseResult<TPlayer> Win(TPlayer winningPlayer)
        {
            return new WinLoseResult<TPlayer>(winningPlayer);
        }

        public TPlayer WinningPlayer { get; }

        public WinLoseDrawResult ResultFor(TPlayer player)
        {
            return WinningPlayer.Equals(player) ? WinLoseDrawResult.Win : WinLoseDrawResult.Lose;
        }
    }

}