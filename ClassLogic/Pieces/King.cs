﻿using System;
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
            return MovePositions(position, board).Select(pos => new NormalMove(position, pos));
        }
    }
}