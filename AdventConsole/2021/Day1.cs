using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day1
    {
        public static IEnumerable<int> input = InputReader.ReadLines("Day1").Select(int.Parse);

        public class Puzzle1 : IPuzzle<int>
        {
            public int CalculateAnswer()
            {
                return input.Pairwise((a, b) => a < b).Count(r => r);
            }
        }

        public class Puzzle2 : IPuzzle<int>
        {
            public int CalculateAnswer()
            {
                return input.Triowise((a, b, c) => a + b + c).Pairwise((a, b) => a < b).Count(r => r);
            }
        }
    }
}