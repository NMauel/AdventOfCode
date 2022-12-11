namespace AdventCode.Aoc2022;

public class Day6 : IPuzzleDay
{
    private readonly string message = InputReader.ReadText();

    public object CalculateAnswerPuzzle1() => FindFirstOccurrenceOfDistinctChars(message, 4);

    public object CalculateAnswerPuzzle2() => FindFirstOccurrenceOfDistinctChars(message, 14);

    private static int FindFirstOccurrenceOfDistinctChars(string datastream, int length)
    {
        for (var i = 0; i < datastream.Count() - length; i++)
            if (datastream.Skip(i).Take(length).Distinct().Count() == length)
                return i + length;
        return 0;
    }
}