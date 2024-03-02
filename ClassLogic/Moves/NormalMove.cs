using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public  class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Position From { get; }
        public override Position ToPos { get; }

        public NormalMove(Position from, Position to)
        {
            From = from;
            ToPos = to;
        }

        public override void Execute(Board board)
        {
            Piece piece = board[From];
            board[ToPos] = piece;
            board[From] = null;
            piece.HasMoved = true;
        }
    }
}
