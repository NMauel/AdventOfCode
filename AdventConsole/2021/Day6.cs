﻿namespace AdventCode.Aoc2021
{
    public class Day6 : IPuzzleDay
    {
        private readonly IEnumerable<short> input = InputReader.ReadText().Split(',').Select(short.Parse);

        public object CalculateAnswerPuzzle1() => new FishRegister(input).SleepOvernight(80).Sum();
        public object CalculateAnswerPuzzle2() => new FishRegister(input).SleepOvernight(256).Sum();

        private class FishRegister : ShiftRegister
        {
            public FishRegister(IEnumerable<short> input) : base(9)
            {
                foreach (var group in input.GroupBy(i => i))
                {
                    base[group.Key] = (ulong)group.Count();
                }
            }

            public FishRegister SleepOvernight(int nights)
            {                
                for (int d = 1; d <= nights; d++)
                {
                    ShiftRight();
                    this[6] += this[8];
                }
                return this;
            }
        }

        protected class ShiftRegister
        {
            private int index = 0;
            public ulong[] fields;

            public ShiftRegister(int size) => fields = new ulong[size];

            public void ShiftRight() => index = index < fields.Length - 1 ? index + 1 : 0;

            public ulong this[int key]
            {
                get => fields[(key + index) % fields.Length];
                set => fields[(key + index) % fields.Length] = value;
            }

            public ulong Sum() => fields.Aggregate((a, b) => a + b);
        }
    }
}