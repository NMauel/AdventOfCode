using System.Diagnostics;
namespace AdventCode;

internal class Program
{
    private static void Main(string[] args)
    {
        if (!args.Any() || !DateTime.TryParse(args?.First(), out var date))
        {
            date = DateTime.Today;
        }

        InputReader.Year = date.Year;
        InputReader.Day = date.Day;

        var puzzle = GetExcercise(date.Year, date.Day);
        var stopwatch = Stopwatch.StartNew();

        var answer = puzzle.CalculateAnswerPuzzle1();
        stopwatch.Stop();

        Console.WriteLine($"The answer of puzzle1: {answer}, took {stopwatch.ElapsedMilliseconds}ms ({stopwatch.GetElapsedNanoseconds()} ns)");

        stopwatch.Restart();
        answer = puzzle.CalculateAnswerPuzzle2();
        stopwatch.Stop();

        Console.WriteLine($"The answer of puzzle2: {answer}, took {stopwatch.ElapsedMilliseconds}ms ({stopwatch.GetElapsedNanoseconds()} ns)");
    }

    private static IPuzzleDay GetExcercise(int year, int day)
    {
        var type = Type.GetType($"AdventCode.Aoc{year}.Day{day}", true);
        return Activator.CreateInstance(type) as IPuzzleDay;
    }
}
