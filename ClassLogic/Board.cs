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
            // Placing Black Pawns
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pawn(Player.Black); // Black pawns on the 2nd row
            }

            // Placing White Pawns
            for (int i = 0; i < 8; i++)
            {
                this[6, i] = new Pawn(Player.White); // White pawns on the 7th row
            }

            // Placing the rest of the Black pieces
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black); // Adjust if the king and queen positions based on your board orientation
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            // Placing the rest of the White pieces
            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White); // Adjust if the king and queen positions based on your board orientation
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
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

        public IEnumerable<Position> PiecePositions()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Position pos = new Position(row, col);
                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }

        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }

        public bool IsInCheck(Player player)
        {
            return PiecePositionsFor(player.Opponent()).Any(pos => this[pos].CanCaptureOpponentKing(pos, this));
        }

        public Board Copy()
        {
            Board copy = new Board();
            foreach (Position pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }
            return copy;
        }
    }
}
