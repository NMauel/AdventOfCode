namespace AdventCode.Aoc2022;

public class Day1 : IPuzzleDay
{
    private readonly IEnumerable<string> calories = InputReader.ReadLines();

    public object CalculateAnswerPuzzle1() => SumCaloriesPerElve().Max();

    public object CalculateAnswerPuzzle2() => SumCaloriesPerElve().OrderDescending().Take(3).Sum();

    private IEnumerable<int> SumCaloriesPerElve()
    {
        var index = 0;
        while (index < calories.Count())
            yield return calories.Skip(index).TakeWhile(c => {
                index++;
                return !string.IsNullOrWhiteSpace(c);
            }).Select(int.Parse).Sum();
    }
}
