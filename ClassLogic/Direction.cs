﻿namespace ClassLogic
{
    public class Direction
    {
        public readonly static Direction North = new Direction(-1, 0);
        public readonly static Direction South = new Direction(1, 0);
        public readonly static Direction East = new Direction(0, 1);
        public readonly static Direction West = new Direction(0, -1);
        public readonly static Direction NorthEast = North + East;
        public readonly static Direction NorthWest = North + West;
        public readonly static Direction SouthEast = South + East;
        public readonly static Direction SouthWest = South + West;

        public int RowDelta { get; set; }
        public int ColumnDelta { get; set; }
        public Direction(int rowDelta, int columnDelta) 
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }
        public static Direction operator +(Direction a, Direction b)
        {
            return new Direction(a.RowDelta + b.RowDelta, a.ColumnDelta + b.ColumnDelta);
        }
        public static Direction operator *(int scalar, Direction a)
        {
            return new Direction(scalar * a.RowDelta, scalar * a.ColumnDelta);
        }
    }
}
