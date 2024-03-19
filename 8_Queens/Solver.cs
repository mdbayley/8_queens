using System;
using System.Reflection;

namespace _8_Queens
{
    internal static class Solver
    {
        public static List<List<int>> Solve()
        {
            var results = new List<List<int>>();

            // predefined arrays of each row, column (col), diagonal-forward-slash (dfs) and diagonal-back-slash (dbs)
            // values are unsigned longs, which can each hold a bit for each of the 64 squares
            // This allows an invalidated square to set its bit to 0 without needing to re-evaluate all the squares' contents
            // To determine if a row, col, dfs or dbs alreaqdy has a value, simply check for > 0

            var rows = new[] { 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul };
            var cols = new[] { 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul };
            var dfss = new[] { 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul };
            var dbss = new[] { 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul, 0ul };

            // It is not a "grid" as such, it is a single-dimentional array, but the contents include their logical "column and row"
            var grid = BuildGrid();

            return results;
        }

        private static Square[] BuildGrid()
        {
            var grid = new Square[64];

            for(var index = 0; index < grid.Length; index++)
            {
                var row = CalculateRow(index);
                var col = CalculateCol(index);
                var dfs = CalculateDfs(index);
                var dbs = CalculateDbs(index);

                grid[index] = new Square(index, row, col, dfs, dbs);
            }

            return grid;
        }

        private static int CalculateRow(int index)
        {
            return 0;
        }

        private static int CalculateCol(int index)
        {
            return 0;
        }

        private static int CalculateDfs(int index)
        {
            return 0;
        }

        private static int CalculateDbs(int index)
        {
            return 0;
        }

        private struct Square
        {
            public Square(int index, int row, int col, int dbs, int dfs)
            {
                Index = index;
                HasQueen = false;
                Row = row;
                Col = col;
                Dfs = dfs;
                Dbs = dbs;
            }

            public int Index; // this is likely redundant...
            public bool HasQueen;

            public int Row;
            public int Col;
            public int Dfs;
            public int Dbs;
        }

    }
}
