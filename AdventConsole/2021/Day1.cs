namespace AdventCode.Aoc2021
{
    public class Day1 : IPuzzleDay
    {
        private readonly IEnumerable<int> input = InputReader.ReadLines().Select(int.Parse);

        public object CalculateAnswerPuzzle1() => input.Pairwise((a, b) => a < b).Count(r => r);

        public object CalculateAnswerPuzzle2() => input.Triowise((a, b, c) => a + b + c).Pairwise((a, b) => a < b).Count(r => r);
    }
}