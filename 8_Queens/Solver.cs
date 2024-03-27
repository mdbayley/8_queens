

using System;

namespace _8_Queens
{
    // DFD = Diagonal Forward (like forward-slash)
    // DBK = Diagonal Back (like backslash)

    internal static class Solver
    {
        public static int Solve(int dimension, out List<int[]> solutions)
        {
            if (dimension > 15)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension), $"{dimension} is invalid, index must be less than 16");
            }
            solutions = new List<int[]>();

            var grid = BuildGrid(dimension);

            var rows = Enumerable.Range(0, dimension).Select(_ => new GridMap()).ToArray();
            var cols = Enumerable.Range(0, dimension).Select(_ => new GridMap()).ToArray();
            var dfds = Enumerable.Range(0, dimension * 2 - 1).Select(_ => new GridMap()).ToArray();
            var dbks = Enumerable.Range(0, dimension * 2 - 1).Select(_ => new GridMap()).ToArray();


            // Because every square evaluated should provide every possible solution containing that square,
            // and because every possible solution must contain a square in each row,
            // you only need to evaluate ONE row (first row) to get all possible solutions.

            for (var start = 0; start < dimension; start++)
            {
                rows.Clear();
                cols.Clear();
                dfds.Clear();
                dbks.Clear();

                var queens = new Stack<int>();

                for (var index = start; index < grid.Length; index++)
                {
                    var square = grid[index];

                    // Check nothing in this Row, Col, DFD or DBK
                    if (rows[square.Row].IsClear() && cols[square.Col].IsClear() && dfds[square.Dfd].IsClear() && dbks[square.Dbk].IsClear())
                    {
                        // We have a Queen

                        square.HasQueen = true;

                        AddToGrid(square, rows, cols, dfds, dbks);

                        queens.Push(index);

                        // As we have found a Queen in this row, we can skip to the end of the row
                        index += dimension - 1 - square.Col;
                    }

                    if (queens.Count == dimension)
                    {
                        solutions.Add(queens.ToArray());

                        // skip the rest of this iteration and move to checking backwards...
                        index = grid.Length - 1;
                    }

                    // Only iterate back once we reach the end of the grid
                    if (index == grid.Length - 1)
                    {
                        // Now let's try removing the last Queen, and starting again from the next square
                        // Removing the last Queen will eventually work its way back through the stack as each iteration eventually fails to find a Queen
                        // We stay in the same loop as we are rewinding the index

                        if (queens.Count == 0) break; // Done when no previous Queens on stack

                        // Remove last Queen from State
                        var lastQueen = grid[queens.Pop()];
                        RemoveFromGrid(lastQueen, rows, cols, dfds, dbks);

                        // If last Queen is last square, go back to Queen before that, else it will end prematurely
                        if (lastQueen.Index == grid.Length - 1)
                        {
                            lastQueen = grid[queens.Pop()];
                            RemoveFromGrid(lastQueen, rows, cols, dfds, dbks);
                        }

                        // Don't re-iterate start, because it will iterate to start+1 in this loop,
                        // and start will increment to start+1 in the next loop,
                        // resulting in duplicate solutions
                        if (lastQueen.Index <= start)
                        {
                            break;
                        }

                        // Go back
                        index = lastQueen.Index;
                    }
                }
            }

            return solutions.Count;
        }

        private static void AddToGrid(Square square, GridMap[] rows, GridMap[] cols, GridMap[] dfds, GridMap[] dbks)
        {
            rows[square.Row].Set(square.Index);
            cols[square.Col].Set(square.Index);
            dfds[square.Dfd].Set(square.Index);
            dbks[square.Dbk].Set(square.Index);
        }

        private static void RemoveFromGrid(Square square, GridMap[] rows, GridMap[] cols, GridMap[] dfds, GridMap[] dbks)
        {
            rows[square.Row].Reset(square.Index);
            cols[square.Col].Reset(square.Index);
            dfds[square.Dfd].Reset(square.Index);
            dbks[square.Dbk].Reset(square.Index);
        }

        private static Square[] BuildGrid(int dimension)
        {
            var grid = new Square[dimension * dimension];

            for (var row = 0; row < dimension; row++)
            {
                for (var col = 0; col < dimension; col++)
                {
                    var index = row * dimension + col;
                    grid[index] = new Square(index, row, col, row + col, col - row + dimension - 1);
                }
            }

            return grid;
        }
    }
}
