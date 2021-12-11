using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day11 : IPuzzleDay<int>
    {
        private readonly int[][] input = InputReader.ReadLines("Day11.txt").Select(x => x.Select(c => Convert.ToInt32(c)-48).ToArray()).ToArray();
        private int steps = 0;

        public int CalculateAnswerPuzzle1()
        {
            var flashCount = 0;
            while(steps < 100)
            {
                DoIteration();
                flashCount += input.SelectMany(c => c).Count(c => c == 0);
            }

            return flashCount;
        }

        public int CalculateAnswerPuzzle2()
        {
            while (!input.SelectMany(c => c).All(c => c == 0))
                DoIteration();

            return steps;
        }

        private void DoIteration()
        {
            for (var y = 0; y < input.Length; y++)
                for (var x = 0; x < input[y].Length; x++)
                    input[y][x]++;

            for (var y = 0; y < input.Length; y++)
                for (var x = 0; x < input[y].Length; x++)
                    Flash(x, y);

            steps++;
        }

        private void Flash(int x, int y, bool add = false)
        {
            if (add && input[y][x] > 0)
                input[y][x]++;

            if (input[y][x] > 9)
            {
                input[y][x] = 0;
                for (var dy = y - 1; dy <= y + 1; dy++)
                    for (var dx = x - 1; dx <= x + 1; dx++)
                        if (dx >= 0 && dx < input[y].Length && dy >= 0 && dy < input.Length)
                            Flash(dx, dy, true);
            }
        }
    }
}