namespace AdventCode.Aoc2025;

public class Day3 : IPuzzleDay
{
    private readonly IEnumerable<int[]> batteries = InputReader.ReadLines().Select(x => x.ToCharArray().Select(c => c - '0').ToArray());

    public object CalculateAnswerPuzzle1() => CalculateSumOfHighestJoltages(batteries, 2);
    
    public object CalculateAnswerPuzzle2() => CalculateSumOfHighestJoltages(batteries, 12);
    
    private static long CalculateSumOfHighestJoltages(IEnumerable<int[]> batteries, int nrOfIntegers) =>
        batteries.Sum(battery => FindHighestJoltage(battery, nrOfIntegers));
    
    private static long FindHighestJoltage(int[] battery, int nrOfIntegers)
    {
        long joltage = 0;
        var startIndex = 0;

        for (var i = 1; i <= nrOfIntegers; i++)
        {
            var number = battery.Skip(startIndex).SkipLast(nrOfIntegers - i).Max();
            startIndex = Array.IndexOf(battery, number, startIndex) + 1;
            joltage += (long)Math.Pow(10, nrOfIntegers - i) * number;
        }

        return joltage;
    }
}
