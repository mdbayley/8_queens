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

            return results;
        }
    }
}
