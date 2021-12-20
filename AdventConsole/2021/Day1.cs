using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day1 : IPuzzleDay<int>
    {
        private readonly IEnumerable<int> input = InputReader.ReadLines("Day1.txt").Select(int.Parse);

        public int CalculateAnswerPuzzle1()
        {
            return input.Pairwise((a, b) => a < b).Count(r => r);
        }

        public int CalculateAnswerPuzzle2()
        {
            return input.Triowise((a, b, c) => a + b + c).Pairwise((a, b) => a < b).Count(r => r);
        }
    }
}