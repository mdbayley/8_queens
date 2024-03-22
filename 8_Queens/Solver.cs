#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
namespace _8_Queens
{
    // DFS = Diagonal Forward Slash
    // DBS = Diagonal Back Slash

    internal static class Solver
    {
        public static int Solve(int dimension, out List<int[]> solutions, bool debug = false)
        {
            solutions = new List<int[]>();

            var grid = BuildGrid(dimension);

            
            // Because every square evaluated should provide every possible solution containing that square,
            // and because every possible solution must contain a square in each row,
            // you only need to evaluate ONE row (first row) to get all possible solutions. TBD

            for (var start = 0; start < dimension; start++)
            {
                if (debug)
                {
                    Console.WriteLine($"=== {start} ===");
                }

                var rows = Enumerable.Repeat(0ul, dimension).ToArray();
                var cols = Enumerable.Repeat(0ul, dimension).ToArray();
                var dfss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();
                var dbss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();

                var queens = new Stack<int>();

                for (var index = start; index < grid.Length; index++)
                {
                    var square = grid[index];

                    if (debug)
                    {
                        Console.WriteLine($"{index} ({square.Row}, {square.Col}) [{start}] {square.ToString(rows, cols, dfss, dbss)}");
                    }

                    if (rows[square.Row] == 0 && cols[square.Col] == 0 && dfss[square.Dfs] == 0 && dbss[square.Dbs] == 0)
                    {
                        // We have a Queen

                        queens.Push(index);

                        square.HasQueen = true;

                        if (debug && square.HasQueen)
                        {
                            Console.WriteLine($"Q{queens.Count}");
                        }

                        AddToGrid(square, rows, cols, dfss, dbss);
                    }

                    if (queens.Count == dimension)
                    {
                        solutions.Add(queens.ToArray());

                        if (debug)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"FOUND SOLUTION: {string.Join(',', queens)}");
                            Console.WriteLine();
                        }

                        // skip the rest of this iteration and move to checking backwards...
                        index = grid.Length - 1;
                    }

                    // Only iterate back once we reach the end of the grid
                    if (index == grid.Length - 1)
                    {
                        // Now let's try removing the last Queen, and starting again from the next square
                        // Removing the last Queen will eventually work its way back through the stack as each iteration eventually fails to find a Queen
                        // We stay in the same loop as we are rewinding the index

                        if (queens.Count == 0) break;

                        if (debug)
                        {
                            Console.WriteLine($"BEFORE: {string.Join(',', queens)}");
                        }

                        // Remove last Queen from State
                        var lastQueen = grid[queens.Pop()];
                        RemoveFromGrid(lastQueen, rows, cols, dfss, dbss);


                        // If last Queen is last square, go back to Queen before
                        if (lastQueen.Index == grid.Length - 1)
                        {
                            RemoveFromGrid(lastQueen, rows, cols, dfss, dbss);
                            lastQueen = grid[queens.Pop()];
                        }

                        if (debug)
                        {
                            Console.WriteLine($"AFTER: {string.Join(',', queens)}");
                            Console.WriteLine($"!!!!! {lastQueen.ToString(rows, cols, dfss, dbss)}");
                        }

                        if (lastQueen.Index <= start)
                        {
                            break;
                        }

                        index = lastQueen.Index;

                        if (debug)
                        {
                            Console.WriteLine($"!!!!! {lastQueen.ToString(rows, cols, dfss, dbss)}");
                            Console.WriteLine($"<<<<< Going back to {index} {lastQueen.ToString(rows, cols, dfss, dbss)}");
                        }
                    }
                }
            }

            return solutions.Count;
        }

        private static void AddToGrid(Square square, ulong[] rows, ulong[] cols, ulong[] dfss, ulong[] dbss)
        {
            var bit = (ulong)Math.Pow(2, square.Index);

            rows[square.Row] |= bit; // Set Row to contain a Queen
            cols[square.Col] |= bit; // Set Col to contain a Queen
            dfss[square.Dfs] |= bit; // Set Dfs to contain a Queen
            dbss[square.Dbs] |= bit; // Set Dbs to contain a Queen
        }

        private static void RemoveFromGrid(Square square, ulong[] rows, ulong[] cols, ulong[] dfss, ulong[] dbss)
        {
            var bit = (ulong)Math.Pow(2, square.Index);
            
            rows[square.Row] &= ~bit; // Remove last Queen from Row
            cols[square.Col] &= ~bit; // Remove last Queen from Col
            dfss[square.Dfs] &= ~bit; // Remove last Queen from DFS
            dbss[square.Dbs] &= ~bit; // Remove last Queen from DBS
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
