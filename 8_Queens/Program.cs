using System.Diagnostics;

namespace _8_Queens
{
    internal class Program
    {
            public static void Main(string[] args)
            {
                var showSolutions = args.Length > 0;

                //Solve(4, showSolutions, true);
                Solve(5, showSolutions, true);
                //Solve(6, showSolutions, true);
                //Solve(7, showSolutions, true);
                //Solve(8, showSolutions, true);
            }

            private static void Solve(int dimension, bool showSolutions, bool debug)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var count = Solver.Solve(dimension, out var solutions, debug);
                stopwatch.Stop();

                Console.WriteLine($"For {dimension} there are {count} solutions [{stopwatch.ElapsedMilliseconds:#,##0} ms]");

                if (showSolutions)
                {
                    foreach (var solution in solutions)
                    {
                        Console.WriteLine(string.Join(',', solution));
                    }
                }

                Console.WriteLine("--------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An ERROR occurred solving {dimension}\n{ex}");
            }
        }
    }
}