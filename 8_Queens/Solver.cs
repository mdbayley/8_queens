
using System.Collections;

namespace _8_Queens
{
    // DFS = Diagonal Forward Slash
    // DBS = Diagonal Back Slash

    internal static class Solver
    {
        public static int Solve(int dimension, out List<int[]> solutions)
        {
            solutions = new List<int[]>();

            var grid = BuildGrid(dimension);
            
            // Because every square evaluated should provide every possible solution containing that square,
            // and because every possible solution must contain a square in each row,
            // you only need to evaluate ONE row (first row) to get all possible solutions.

            for (var start = 0; start < dimension; start++)
            {
                // Initialise rows, columns and diagonals for each square in this iteration

                var rows = Enumerable.Range(0, dimension).Select(_ => new BitArray(grid.Length)).ToArray();
                var cols = Enumerable.Range(0, dimension).Select(_ => new BitArray(grid.Length)).ToArray();
                var dfss = Enumerable.Range(0, dimension * 2 - 1).Select(_ => new BitArray(grid.Length)).ToArray();
                var dbss = Enumerable.Range(0, dimension * 2 - 1).Select(_ => new BitArray(grid.Length)).ToArray();

                var queens = new Stack<int>();

                for (var index = start; index < grid.Length; index++)
                {
                    var square = grid[index];

                    var r = rows[square.Row].HasAnySet();
                    var c = cols[square.Col].HasAnySet();
                    var f = dfss[square.Dfs].HasAnySet();
                    var b = dbss[square.Dbs].HasAnySet();

                    // Check nothing in this Row, Col, DFS or DBS
                    if (!r && !c && !f && !b)
                    {
                        // We have a Queen

                        square.HasQueen = true;

                        AddToGrid(square, rows, cols, dfss, dbss);

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
                        RemoveFromGrid(lastQueen, rows, cols, dfss, dbss);

                        // If last Queen is last square, go back to Queen before that, else it will end prematurely
                        if (lastQueen.Index == grid.Length - 1)
                        {
                            lastQueen = grid[queens.Pop()];
                            RemoveFromGrid(lastQueen, rows, cols, dfss, dbss);
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

        private static void AddToGrid(Square square, BitArray[] rows, BitArray[] cols, BitArray[] dfss, BitArray[] dbss)
        {
            rows[square.Row].Set(square.Index, true); // Set Row to contain a Queen
            cols[square.Col].Set(square.Index, true); // Set Col to contain a Queen
            dfss[square.Dfs].Set(square.Index, true); // Set Dfs to contain a Queen
            dbss[square.Dbs].Set(square.Index, true); // Set Dbs to contain a Queen
        }

        private static void RemoveFromGrid(Square square, BitArray[] rows, BitArray[] cols, BitArray[] dfss, BitArray[] dbss)
        {
            rows[square.Row].Set(square.Index, false); // Remove Queen from Row
            cols[square.Col].Set(square.Index, false); // Remove Queen from Col
            dfss[square.Dfs].Set(square.Index, false); // Remove Queen from DFS
            dbss[square.Dbs].Set(square.Index, false); // Remove Queen from DBS
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
