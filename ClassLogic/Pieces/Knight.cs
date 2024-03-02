using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; set; }

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(this.Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotentialToPositions(Position from)
        {
            foreach (Direction vDir in new Direction[] { Direction.North, Direction.South })
            {
                foreach (Direction hDir in new Direction[] { Direction.East, Direction.West })
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + vDir + 2 * hDir;
                }
            }
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            return PotentialToPositions(from).Where(pos => Board.IsInsideBoard(pos) && (board.IsEmpty(pos) || board[pos].Color != this.Color));
        }

        public override IEnumerable<Move> GetMoves(Position position, Board board)
        {
            return MovePositions(position, board).Select(pos => new NormalMove(position, pos));
        }
    }
}
