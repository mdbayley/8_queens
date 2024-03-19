using System.Diagnostics;
using _8_Queens;

var stopwatch = Stopwatch.StartNew();

var results = Solver.Solve();

Console.WriteLine($"{results.Count} results, {stopwatch.ElapsedMilliseconds:#,##0} ms");
Console.WriteLine("---");
foreach (var result in results)
{
    Console.WriteLine(string.Join(',', result));
}