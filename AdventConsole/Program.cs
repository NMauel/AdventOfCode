using System;
using System.Diagnostics;

namespace AdventCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new Day6();

            var stopwatch = Stopwatch.StartNew();
            var answer = puzzle.CalculateAnswerPuzzle1();
            stopwatch.Stop();

            Console.WriteLine($"The answer of puzzle1: {answer}, took {stopwatch.ElapsedMilliseconds}ms ({ stopwatch.GetElapsedNanoseconds() } ns)");

            stopwatch.Restart();
            answer = puzzle.CalculateAnswerPuzzle2();
            stopwatch.Stop();

            Console.WriteLine($"The answer of puzzle2: {answer}, took {stopwatch.ElapsedMilliseconds}ms ({ stopwatch.GetElapsedNanoseconds() } ns)");
        }
    }

    static class StopwatchExtensions
    {
        public static long GetElapsedNanoseconds(this Stopwatch stopwatch)
        {
            var ticks = stopwatch.ElapsedTicks;
            var frequency = Stopwatch.Frequency;

            var nanosecondspertick = (1000L * 1000L * 1000L) / frequency;
            return ticks * nanosecondspertick;
        }
    }
}
