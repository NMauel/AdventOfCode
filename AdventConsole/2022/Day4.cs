namespace AdventCode.Aoc2022;

public class Day4 : IPuzzleDay
{
    private readonly IEnumerable<Camp> input = InputReader.ReadLines().Select(ParseToCamp);

    public object CalculateAnswerPuzzle1() => input.Count(c => 
        c.ID1_min <= c.ID2_min && c.ID1_max >= c.ID2_max ||
        c.ID1_min >= c.ID2_min && c.ID1_max <= c.ID2_max);

    public object CalculateAnswerPuzzle2() => input.Count(c => c.ID1_max >= c.ID2_min && c.ID1_min <= c.ID2_max);

    record Camp(int ID1_min, int ID1_max, int ID2_min, int ID2_max);
    
    static Camp ParseToCamp(string line)
    {
        var ids = line.Split(',', '-').Select(int.Parse).ToArray();
        return new Camp(ids[0], ids[1], ids[2], ids[3]);
    }
}