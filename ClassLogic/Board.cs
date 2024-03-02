using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];
        public Piece this[int x, int y]
        {
            get
            {
                return pieces[x, y];
            }
            set
            {
                pieces[x, y] = value;
            }
        }

        public Piece this[Position position]
        {
            get
            {
                return pieces[position.Row, position.Column];
            }
            set
            {
                pieces[position.Row, position.Column] = value;
            }
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                this[i, 1] = new Pawn(Player.Black);
                this[i, 6] = new Pawn(Player.White);
            }
            this[0, 0] = new Rook(Player.Black);
            this[1, 0] = new Knight(Player.Black);
            this[2, 0] = new Bishop(Player.Black);
            this[3, 0] = new Queen(Player.Black);
            this[4, 0] = new King(Player.Black);
            this[5, 0] = new Bishop(Player.Black);
            this[6, 0] = new Knight(Player.Black);
            this[7, 0] = new Rook(Player.Black);

            this[0, 7] = new Rook(Player.White);
            this[1, 7] = new Knight(Player.White);
            this[2, 7] = new Bishop(Player.White);
            this[3, 7] = new Queen(Player.White);
            this[4, 7] = new King(Player.White);
            this[5, 7] = new Bishop(Player.White);
            this[6, 7] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);
        }

        public static bool IsInsideBoard(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
        }

        public bool IsEmpty(Position position)
        {
            return IsInsideBoard(position) && this[position.Row, position.Column] == null;
        }
    }
}
