using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; set; }
        private static Direction[] dirs = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.NorthEast, Direction.NorthWest, Direction.SouthEast, Direction.SouthWest };

        public Queen(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Queen copy = new Queen(this.Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }
        public override IEnumerable<Move> GetMoves(Position position, Board board)
        {
            return MovePositionsInDirs(position, board, dirs).Select(pos => new NormalMove(position, pos));
        }
    }
}
