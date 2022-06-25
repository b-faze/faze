using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Examples.Games.Rubik
{
    public class RubikState : IGameState<GridMove, RubikResult?>
    {

        // mapped to a 4x4 board
        private static readonly RubikMove?[] rubikMoveMapping = GetRubikMoveMapping();
        private static readonly GridMove[] gridMoves = GetRubikMoveMapping()
            .Where(r => r.HasValue)
            .Select(r => ToGridMove(r.Value))
            .ToArray();

        private RubikCube cube;
        public static RubikState InitialSolved => new RubikState(RubikCube.Solved());

        private RubikState(RubikCube cube) 
        {
            this.cube = cube;
        }

        public PlayerIndex CurrentPlayerIndex => PlayerIndex.P1;

        public IEnumerable<GridMove> GetAvailableMoves() => gridMoves;

        public RubikResult? GetResult() => cube.IsSolved() ? RubikResult.Solved : (RubikResult?)null;

        public IGameState<GridMove, RubikResult?> Move(GridMove move)
        {
            var rubikMove = ToRubikMove(move);
            return new RubikState(cube.Move(rubikMove));
        }

        private static GridMove ToGridMove(RubikMove move)
        {
            for (var i = 0; i < rubikMoveMapping.Length; i++)
            {
                var rubikMove = rubikMoveMapping[i];
                if (rubikMove.HasValue && rubikMove.Value.Face == move.Face && rubikMove.Value.Direction == move.Direction)
                {
                    return i;
                }
            }

            throw new NotSupportedException($"Unknown move face '{move.Face}'");
        }

        private static RubikMove ToRubikMove(GridMove move)
        {
            var rubikMove = rubikMoveMapping[move];
            if (rubikMove.HasValue)
            {
                return rubikMove.Value;
            }

            throw new NotSupportedException($"Unknown grid move '{move}'");
        }

        private static RubikMove?[] GetRubikMoveMapping() {
            return new RubikMove?[]
            {
                null,
                new RubikMove(RubikMoveFace.Top, RubikMoveDirection.Anticlockwise),
                new RubikMove(RubikMoveFace.Top, RubikMoveDirection.Clockwise),
                null,
                new RubikMove(RubikMoveFace.Left, RubikMoveDirection.Clockwise),
                new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Anticlockwise),
                new RubikMove(RubikMoveFace.Front, RubikMoveDirection.Clockwise),
                new RubikMove(RubikMoveFace.Right, RubikMoveDirection.Clockwise),
                new RubikMove(RubikMoveFace.Left, RubikMoveDirection.Anticlockwise),
                new RubikMove(RubikMoveFace.Back, RubikMoveDirection.Anticlockwise),
                new RubikMove(RubikMoveFace.Back, RubikMoveDirection.Clockwise),
                new RubikMove(RubikMoveFace.Right, RubikMoveDirection.Anticlockwise),
                null,
                new RubikMove(RubikMoveFace.Bottom, RubikMoveDirection.Anticlockwise),
                new RubikMove(RubikMoveFace.Bottom, RubikMoveDirection.Clockwise),
                null
            };
        }
    }
}
