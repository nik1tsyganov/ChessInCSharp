using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public  class GameState
    {
        public Board Board { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public GameState()
        {
            Board = Board.Initial();
            CurrentPlayer = Player.White;
        }

        public GameState(Player currentPlayer, Board board)
        {
            Board = board;
            CurrentPlayer = currentPlayer;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board[pos] == null || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }
            
            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
        }
    }
}
