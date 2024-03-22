
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

            //var rows = Enumerable.Repeat(0ul, dimension).ToArray();
            //var cols = Enumerable.Repeat(0ul, dimension).ToArray();
            //var dfss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();
            //var dbss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();

            //* Console.WriteLine($"Rows={rows.Length}, Columns={cols.Length}, DFSs={dfss.Length}, DBSs={dbss.Length}");
            
            // It is not a "grid" as such, it is a single-dimentional array, but the contents include their logical "column and row"
            var grid = BuildGrid(dimension);

            
            // Because every square evaluated should provide every possible solution containing that square,
            // and because every possible solution must contain a square in each row,
            // you only need to evaluate ONE row (first row) to get all possible solutions. TBD

            for (var start = 0; start < dimension; start++)
            {
                var rows = Enumerable.Repeat(0ul, dimension).ToArray();
                var cols = Enumerable.Repeat(0ul, dimension).ToArray();
                var dfss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();
                var dbss = Enumerable.Repeat(0ul, dimension * 2 - 1).ToArray();

                var queens = new List<int>();

                for (var index = start; index < grid.Length; index++)
                {
                    //* Console.WriteLine($"--- {start},{index} ---");

                    var square = grid[index];

                    //* Console.WriteLine(square.ToString(rows, cols, dfss, dbss));

                    if (rows[square.Row] == 0 && cols[square.Col] == 0 && dfss[square.Dfs] == 0 && dbss[square.Dbs] == 0)
                    {
                        // We have a Queen

                        queens.Add(index);
                        //* Console.WriteLine($">>> {queens.Count}");

                        square.HasQueen = true;

                        var bit = (ulong)Math.Pow(2, index);
                        //* Console.WriteLine($"BIT: {bit} [{Convert.ToString((long)bit, 2)}]");

                        rows[square.Row] |= bit; // Set Row to contain a Queen
                        cols[square.Col] |= bit; // Set Col to contain a Queen
                        dfss[square.Dfs] |= bit; // Set Dfs to contain a Queen
                        dbss[square.Dbs] |= bit; // Set Dbs to contain a Queen

                        //* Console.WriteLine(square.ToString(rows, cols, dfss, dbss));
                        //* Console.WriteLine();

                        if (queens.Count == dimension)
                        {
                            // We have a solution
                            solutions.Add(queens.ToArray());
                            //* Console.WriteLine("*******");
                            break;
                        }
                    }

                    //* Console.WriteLine();
                }

                //* Console.WriteLine();
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
