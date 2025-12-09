namespace AdventCode.Aoc2025;

public class Day1 : IPuzzleDay
{
    private readonly IEnumerable<int> rotations = InputReader.ReadLines().Select(x => int.Parse(x.Replace("R", "").Replace("L", "-")));

    public object CalculateAnswerPuzzle1()
    {
        var dial = 50;
        var timesOnZero = 0;

        foreach (var rotation in rotations)
        {
            dial += rotation;
            if (dial % 100 == 0)
            {
                timesOnZero++;
            }
        }
        return timesOnZero;
    }

    public object CalculateAnswerPuzzle2()
    {
        var dial = 50;
        var timesSkippedOrOnZero = 0;

        foreach (var rotation in rotations)
        {
            var isNegative = Math.Sign(rotation) == -1;

            for (var i = 1; i <= (isNegative ? -rotation : rotation); i++)
            {
                if (isNegative)
                    dial -= 1;
                else
                    dial += 1;
                if (dial % 100 == 0)
                {
                    timesSkippedOrOnZero++;
                }
            }
        }
        return timesSkippedOrOnZero;
    }
}
