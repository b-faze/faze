using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.GameMoves
{
    /// <summary>
    /// Represents a move associated with a tile on a 2D grid. 
    /// </summary>
    public struct GridMove
    {
        private int index;

        public GridMove(int index)
        {
            this.index = index;
        }

        public GridMove(int x, int y, int width)
        {
            this.index = x + y * width;
        }

        public static implicit operator int(GridMove playerIndex)
        {
            return playerIndex.index;
        }

        public static implicit operator GridMove(int index)
        {
            return new GridMove(index);
        }

        public override string ToString()
        {
            return index.ToString();
        }
    }
}
