using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; set; }
        private static Direction[] dirs = new Direction[] { Direction.NorthEast, Direction.NorthWest, Direction.SouthEast, Direction.SouthWest };

        public Bishop(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Bishop copy = new Bishop(this.Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position position, Board board)
        {
            return MovePositionsInDirs(position, board, dirs).Select(pos => new NormalMove(position, pos));
        }
    }
}
