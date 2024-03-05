using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class PawnPromotion : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position From { get; }
        public override Position ToPos { get; }
        public PieceType newType { get; }

        public PawnPromotion(Position from, Position to, PieceType promoteTo)
        {
            From = from;
            ToPos = to;
            newType = promoteTo;
        }

        private Piece CreatePromotionPiece(Player color)
        {
            return newType switch
            {
                PieceType.Rook => new Rook(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Knight => new Knight(color),
                _ => new Queen(color)
            };
        }

        public override void Execute(Board board)
        {
            Piece piece = board[From];
            board[ToPos] = CreatePromotionPiece(piece.Color);
            board[From] = null;
            board[ToPos].HasMoved = true;
        }
    }
}
