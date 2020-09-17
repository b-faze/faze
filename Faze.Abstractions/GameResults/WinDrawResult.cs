namespace Faze.Abstractions.GameResults
{

    public class WinDrawResult<TPlayer>
    {
        private WinDrawResult(TPlayer winningPlayer)
        {
            WinningPlayer = winningPlayer;
            IsDraw = false;
        }

        private WinDrawResult()
        {
            IsDraw = true;
        }

        public static WinDrawResult<TPlayer> Win(TPlayer winningPlayer)
        {
            return new WinDrawResult<TPlayer>(winningPlayer);
        }

        public static WinDrawResult<TPlayer> Draw() => new WinDrawResult<TPlayer>();

        public bool IsDraw { get; }
        public TPlayer WinningPlayer { get; }

        public WinLoseDrawResult ResultFor(TPlayer player)
        {
            if (IsDraw) return WinLoseDrawResult.Draw;

            return WinningPlayer.Equals(player) ? WinLoseDrawResult.Win : WinLoseDrawResult.Lose;
        }
    }

}