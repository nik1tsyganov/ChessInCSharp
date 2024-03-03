using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position From { get; }
        public abstract Position ToPos { get; }
        public abstract void Execute(Board board);

        // Perfect for normal move but for other moves, it should be overridden
        public virtual bool IsLegal(Board board)
        {
            Player player = board[From].Color;
            Board copy = board.Copy();
            Execute(copy);
            return !copy.IsInCheck(player);
        }
    }
}
