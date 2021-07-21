namespace Faze.Examples.Games.GridGames
{
    public class PiecesBoardStateConfig
    {
        public PiecesBoardStateConfig(int dimension, IPiece piece, bool onlySafeMoves = false)
        {
            Dimension = dimension;
            Piece = piece;
            OnlySafeMoves = onlySafeMoves;
        }

        public int Dimension { get; }
        public IPiece Piece { get; }

        /// <summary>
        /// If true, the available moves will be filtered to correct moves
        /// </summary>
        public bool OnlySafeMoves { get; }
    }

}
