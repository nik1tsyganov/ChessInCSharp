using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class Result
    {
        public EndReason Reason { get; private set; }
        public Player Winner { get; private set; }

        public Result(EndReason reason, Player winner)
        {
            Reason = reason;
            Winner = winner;
        }

        public static Result Win(Player winner)
        {
            return new Result(EndReason.Checkmate, winner);
        }

        public static Result Draw(EndReason reason)
        {
            return new Result(reason, Player.None);
        }
    }
}
