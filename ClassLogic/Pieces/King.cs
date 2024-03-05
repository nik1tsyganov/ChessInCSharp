using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; set; }
        private static Direction[] dirs = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.NorthEast, Direction.NorthWest, Direction.SouthEast, Direction.SouthWest };

        public King(Player color)
        {
            Color = color;
        }

        private static bool IsUnmovedRook(Position pos, Board board)
        {
            return board[pos] != null && board[pos].Type == PieceType.Rook && !board[pos].HasMoved;
        }

        private static bool AllEmpty(IEnumerable<Position> positions, Board board)
        {
            return positions.All(pos => board.IsEmpty(pos));
        }

        private bool CanCastleKingSide(Position kingPos, Board board)
        {
            return !this.HasMoved && IsUnmovedRook(new Position(kingPos.Row, 7), board) && AllEmpty(new Position[] { new Position(kingPos.Row, 5), new Position(kingPos.Row, 6) }, board);
        }

        private bool CanCastleQueenSide(Position kingPos, Board board)
        {
            return !this.HasMoved && IsUnmovedRook(new Position(kingPos.Row, 0), board) && AllEmpty(new Position[] { new Position(kingPos.Row, 1), new Position(kingPos.Row, 2), new Position(kingPos.Row, 3) }, board);
        }

        public override Piece Copy()
        {
            King copy = new King(this.Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position pos = from + dir;
                if (Board.IsInsideBoard(pos) && (board.IsEmpty(pos) || board[pos].Color != this.Color))
                {
                    yield return pos;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position position, Board board)
        {
            foreach (Position pos in MovePositions(position, board))
            {
                yield return new NormalMove(position, pos);
            }

            if (CanCastleKingSide(position, board))
            {
                yield return new Castle(MoveType.CastleKS, position);
            }

            if (CanCastleQueenSide(position, board))
            {
                yield return new Castle(MoveType.CastleQS, position);
            }
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(pos => board[pos] != null && board[pos].Type == PieceType.King);
        }
    }
}
