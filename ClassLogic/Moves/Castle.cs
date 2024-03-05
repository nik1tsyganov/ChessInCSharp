using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class Castle : Move
    {
        public override MoveType Type { get; }
        public override Position From { get; }
        public override Position ToPos { get; }

        private readonly Direction kingMoveDir;
        private readonly Position RookFromPos;
        private readonly Position RookToPos;

        public Castle(MoveType type, Position kingPos)
        {
            Type = type;
            From = kingPos;

            if (type == MoveType.CastleKS)
            {
                kingMoveDir = Direction.East;
                ToPos = new Position(kingPos.Row, 6);
                RookFromPos = new Position(kingPos.Row, 7);
                RookToPos = new Position(kingPos.Row, 5);
            }
            else if (type == MoveType.CastleQS)
            {
                kingMoveDir = Direction.West;
                ToPos = new Position(kingPos.Row, 2);
                RookFromPos = new Position(kingPos.Row, 0);
                RookToPos = new Position(kingPos.Row, 3);
            }
        }

        public override void Execute(Board board)
        {
            new NormalMove(From, ToPos).Execute(board);
            new NormalMove(RookFromPos, RookToPos).Execute(board);
        }

        public override bool IsLegal(Board board)
        {
            Player player = board[From].Color;
            if (board.IsInCheck(player))
            {
                return false;
            }

            Board copy = board.Copy();
            Position kingPos = From;

            for (int i = 0; i < 2; i++)
            {
                new NormalMove(kingPos, kingPos + kingMoveDir).Execute(copy);
                if (copy.IsInCheck(player))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
