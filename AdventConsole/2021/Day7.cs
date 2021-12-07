using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day7 : IPuzzleDay<int>
    {
        private IEnumerable<int> input = InputReader.ReadString("Day7.txt").Split(',').Select(int.Parse);

        public int CalculateAnswerPuzzle1()
        {
            var leastPossibleFuel = int.MaxValue;

            for (int position = input.Min(); position <= input.Max(); position++)
                leastPossibleFuel = Math.Min(leastPossibleFuel, input.Sum(crab => Math.Abs(position - crab)));

            return leastPossibleFuel;
        }

        public int CalculateAnswerPuzzle2()
        {
            var leastPossibleFuel = int.MaxValue;

            for (int position = input.Min(); position <= input.Max(); position++)
                leastPossibleFuel = Math.Min(leastPossibleFuel, input.Sum(crab => (Math.Abs(position - crab) * (Math.Abs(position - crab) + 1)) / 2));

            return leastPossibleFuel;
        }
    }
}