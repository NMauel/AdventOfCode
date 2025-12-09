using System.Diagnostics;
namespace AdventCode;

internal static class StopwatchExtensions
{
    public static long GetElapsedNanoseconds(this Stopwatch stopwatch)
    {
        var ticks = stopwatch.ElapsedTicks;
        var frequency = Stopwatch.Frequency;

        var nanosecondspertick = 1000L * 1000L * 1000L / frequency;
        return ticks * nanosecondspertick;
    }
}
