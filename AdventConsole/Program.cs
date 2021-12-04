using System;
using System.Diagnostics;

namespace AdventCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new Day3.Puzzle2();

            var stopwatch = Stopwatch.StartNew();
            var answer = puzzle.CalculateAnswer();
            stopwatch.Stop();

            Console.WriteLine($"The answer: {answer}, took {stopwatch.ElapsedMilliseconds}ms ({ stopwatch.GetElapsedNanoseconds() } ns)");
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
