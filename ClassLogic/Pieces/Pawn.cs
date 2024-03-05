using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; set; }
        private readonly Direction forward;

        public Pawn(Player color)
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Direction.North;
            }
            else
            {
                forward = Direction.South;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(this.Color);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position pos, Board board)
        {
            return board.IsEmpty(pos) && Board.IsInsideBoard(pos);
        }

        private bool CanCaptureAt(Position pos, Board board)
        {
            if (!Board.IsInsideBoard(pos) || board.IsEmpty(pos))
            {
                return false;
            }
            return board[pos].Color != this.Color;
        }

        private IEnumerable<Move> FordwardMoves(Position pos, Board board)
        {
            Position next = pos + forward;
            if (CanMoveTo(next, board))
            {
                if (next.Row == 0 || next.Row == 7)
                {
                    foreach (Move move in PromotionMoves(pos, next))
                    {
                        yield return move;
                    }
                }
                else  yield return new NormalMove(pos, next);

                if (!HasMoved)
                {
                    next += forward;
                    if (CanMoveTo(next, board))
                    {
                        yield return new NormalMove(pos, next);
                    }
                }
            }
        }

        private IEnumerable<Move> DiagonalMoves(Position pos, Board board)
        {
            Position left = pos + forward + Direction.West;
            Position right = pos + forward + Direction.East;
            if (CanCaptureAt(left, board))
            {
                if (left.Row == 0 || left.Row == 7)
                {
                    foreach (Move move in PromotionMoves(pos, left))
                    {
                        yield return move;
                    }
                }
                else yield return new NormalMove(pos, left);
            }
            if (CanCaptureAt(right, board))
            {
                if (right.Row == 0 || right.Row == 7)
                {
                    foreach (Move move in PromotionMoves(pos, right))
                    {
                        yield return move;
                    }
                }
                else yield return new NormalMove(pos, right);
            }
        }

        public override IEnumerable<Move> GetMoves(Position position, Board board)
        {
            return FordwardMoves(position, board).Concat(DiagonalMoves(position, board));
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return DiagonalMoves(from, board).Any(move => board[move.ToPos] != null && board[move.ToPos].Type == PieceType.King);
        }

        private static IEnumerable<Move> PromotionMoves(Position from, Position to)
        {
            yield return new PawnPromotion(from, to, PieceType.Queen);
            yield return new PawnPromotion(from, to, PieceType.Rook);
            yield return new PawnPromotion(from, to, PieceType.Bishop);
            yield return new PawnPromotion(from, to, PieceType.Knight);
        }
    }
}
