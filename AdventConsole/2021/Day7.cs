namespace AdventCode.Aoc2021
{
    public class Day7 : IPuzzleDay
    {
        private readonly IEnumerable<int> input = InputReader.ReadText().Split(',').Select(int.Parse);

        public object CalculateAnswerPuzzle1()
        {
            var leastPossibleFuel = int.MaxValue;

            for (int position = input.Min(); position <= input.Max(); position++)
                leastPossibleFuel = Math.Min(leastPossibleFuel, input.Sum(crab => Math.Abs(position - crab)));

            return leastPossibleFuel;
        }

        public object CalculateAnswerPuzzle2()
        {
            var leastPossibleFuel = int.MaxValue;

            for (int position = input.Min(); position <= input.Max(); position++)
                leastPossibleFuel = Math.Min(leastPossibleFuel, input.Sum(crab => (Math.Abs(position - crab) * (Math.Abs(position - crab) + 1)) / 2));

            return leastPossibleFuel;
        }
    }
}