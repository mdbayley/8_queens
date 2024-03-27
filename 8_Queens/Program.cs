using System.Diagnostics;

namespace _8_Queens
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var showSolutions = args.Length > 0;

                Solve(4, showSolutions);
                
                Solve(5, showSolutions);
                
                Solve(6, showSolutions);
                
                Solve(7, showSolutions);
                
                Solve(8, showSolutions);
                
                Solve(9, showSolutions);
                
                Solve(10, showSolutions);
                
                Solve(11, showSolutions);
                
                Solve(12, showSolutions);
            }
            finally
            {
                if (!Debugger.IsAttached)
                {
                    Console.WriteLine("Done.");
                    Console.ReadKey();
                }
            }
        }

        private static void Solve(int dimension, bool showSolutions)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var count = Solver.Solve(dimension, out var solutions);
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