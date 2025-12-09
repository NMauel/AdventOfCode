namespace AdventCode.Aoc2023;

public class Day1 : IPuzzleDay
{
    private readonly IEnumerable<string> calibrationValues = InputReader.ReadLines();

    public object CalculateAnswerPuzzle1() => calibrationValues.Sum(x => x.FirstNumeric() * 10 + x.LastNumeric());

    public object CalculateAnswerPuzzle2() => calibrationValues.ReplaceWordsToNumeric().Sum(x => x.FirstNumeric() * 10 + x.LastNumeric());
}

public static class Day1Extensions
{
    public static int FirstNumeric(this string calibration) => Convert.ToInt32(calibration.First(x => x >= '0' && x <= '9') - 48);

    public static int LastNumeric(this string calibration) => Convert.ToInt32(calibration.Last(x => x >= '0' && x <= '9') - 48);

    public static IEnumerable<string> ReplaceWordsToNumeric(this IEnumerable<string> calibrationValues)
    {
        foreach (var calibrationValue in calibrationValues)
        {
            yield return calibrationValue.Replace("one", "one1one")
                .Replace("two", "two2two")
                .Replace("three", "three3three")
                .Replace("four", "four4four")
                .Replace("five", "five5five")
                .Replace("six", "six6six")
                .Replace("seven", "seven7seven")
                .Replace("eight", "eight8eight")
                .Replace("nine", "nine9nine");
        }
    }
}
