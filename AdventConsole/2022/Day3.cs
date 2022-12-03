namespace AdventCode.Aoc2022;

public class Day3 : IPuzzleDay
{
    private readonly IEnumerable<string> input = InputReader.ReadLines();

    public object CalculateAnswerPuzzle1() => input.Select(rucksack =>
    {
        var compartiments = rucksack.Chunk(rucksack.Length / 2).ToArray();
        return compartiments[0].Intersect(compartiments[1]).First();
    }).Sum(x => x > 90 ? x - 96 : x - 38);

    public object CalculateAnswerPuzzle2() => 
        input.Chunk(3).Select(group => group[0].Intersect(group[1]).Intersect(group[2]).First()).Sum(x => x > 90 ? x - 96 : x - 38);
}