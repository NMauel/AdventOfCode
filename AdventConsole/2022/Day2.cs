namespace AdventCode.Aoc2022;

public class Day2 : IPuzzleDay
{
    private readonly IEnumerable<string> input = InputReader.ReadLines();

    private readonly int[] matchpoints = new[] { 6, 0, 3 };
    public object CalculateAnswerPuzzle1() => input.Select(x => matchpoints[(x[2] - x[0]) % 3] + x[2] - 87).Sum();

    private readonly int[] handpoints = new[] { 3, 1, 2 };
    public object CalculateAnswerPuzzle2() => input.Select(x => ((x[2] - 88) * 3) + handpoints[(x[0] + x[2]) % 3]).Sum();
}