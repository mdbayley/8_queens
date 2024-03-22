
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
namespace _8_Queens
{
    internal static class Solver
    {
        public static int Solve(int dimension, out List<int[]> solutions)
        {
            solutions = new List<int[]>();

            // predefined arrays of each row, column (col), diagonal-forward-slash (dfs) and diagonal-back-slash (dbs)
            // values are unsigned longs, which can each hold a bit for each of the 64 squares
            // This allows an invalidated square to set its bit to 0 without needing to re-evaluate all the squares' contents
            // To determine if a row, col, dfs or dbs alreaqdy has a value, simply check for > 0

            var rows = Enumerable.Repeat(0ul, dimension).ToArray();
            var cols = Enumerable.Repeat(0ul, dimension).ToArray();
            var dfss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();
            var dbss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();

            Console.WriteLine($"Rows={rows.Length}, Columns={cols.Length}, DFSs={dfss.Length}, DBSs={dbss.Length}");
            
            // It is not a "grid" as such, it is a single-dimentional array, but the contents include their logical "column and row"
            var grid = BuildGrid(dimension);

            // Stack allows going backward
            var queens = new Stack<int>();
            
            // Because every square evaluated should provide every possible solution containing that square,
            // and because every possible solution must contain a square in each row,
            // you only need to evaluate ONE row (first row) to get all possible solutions. TBD

            for (var start = 0; start < dimension; start++)
            {
                for (var index = start; index < grid.Length; index++)
                {
                    var square = grid[index];

                    Console.WriteLine(square);

                    if (rows[square.Row] == 0 && cols[square.Col] == 0 && dfss[square.Dfs] == 0 && dbss[square.Dbs] == 0)
                    {
                        // We have a Queen

                        rows[square.Row] |= (ulong)(2 ^ index); // Set Row to contain a Queen
                        cols[square.Col] |= (ulong)(2 ^ index); // Set Col to contain a Queen
                        dfss[square.Dfs] |= (ulong)(2 ^ index); // Set Dfs to contain a Queen
                        rows[square.Row] |= (ulong)(2 ^ index); // Set Dbs to contain a Queen

                        queens.Push(index);

                        if (queens.Count == dimension)
                        {
                            // We have a solution
                            solutions.Add(queens.ToArray());
                            break;
                        }

                        // We have a Queen in the row, move to the next row
                        // Unless we are in the last row, which means although we found a Queen, we never found a solution

                        if (square.Row == dimension - 1)
                        {
                            // Last row, no solution
                            break;
                        }

                        // Start of next row
                        index = (square.Row + 1) * dimension;
                    }
                }
            }

            return solutions.Count;
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
