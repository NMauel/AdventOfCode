using System.Text.RegularExpressions;
namespace AdventCode.Aoc2022;

public class Day5 : IPuzzleDay
{
    private static readonly Regex regex = new(@"move (\d+) from (\d+) to (\d+)");
    private static readonly IEnumerable<string> input = InputReader.ReadLines();
    private static readonly IEnumerable<(int Amount, int IndexFrom, int IndexTo)> procedures = input.Skip(10).Select(x => {
        var matches = regex.Match(x);
        return (int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value) - 1, int.Parse(matches.Groups[3].Value) - 1);
    });

    public object CalculateAnswerPuzzle1()
    {
        var crateStacks = ParseCrates();

        foreach (var procedure in procedures)
        {
            for (var x = 1; x <= procedure.Amount; x++)
            {
                crateStacks[procedure.IndexTo].Push(crateStacks[procedure.IndexFrom].Pop());
            }
        }

        return string.Concat(crateStacks.Select(x => x.Peek()));
    }

    public object CalculateAnswerPuzzle2()
    {
        var crateStacks = ParseCrates();

        var queue = new Stack<char>();
        foreach (var procedure in procedures)
        {
            for (var x = 1; x <= procedure.Amount; x++)
            {
                queue.Push(crateStacks[procedure.IndexFrom].Pop());
            }
            while (queue.TryPop(out var crate))
                crateStacks[procedure.IndexTo].Push(crate);
        }

        return string.Concat(crateStacks.Select(x => x.Peek()));
    }

    private static List<Stack<char>> ParseCrates()
    {
        var crateStacks = new List<Stack<char>>();
        foreach (var line in input.Take(8).Reverse())
        {
            var index = 0;
            foreach (var crate in line.Chunk(4))
            {
                if (index >= crateStacks.Count())
                    crateStacks.Add(new());
                if (crate[0] == '[')
                    crateStacks[index].Push(crate[1]);
                index++;
            }
        }

        return crateStacks;
    }
}
