using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day14 : IPuzzleDay<long>
    {
        public static readonly Pair[] rules = InputReader.ReadLines("Day14.txt").Select(line => new Pair(line)).ToArray();

        public long CalculateAnswerPuzzle1() => ReinforcePolymer("ONHOOSCKBSVHBNKFKSBK", 10);
        public long CalculateAnswerPuzzle2() => ReinforcePolymer("ONHOOSCKBSVHBNKFKSBK", 40);

        private long ReinforcePolymer(string startTemplate, int steps)
        {
            var polymer = new Polymer(startTemplate);            

            for (var i = 0; i < steps; i++)
                polymer.Reinforce();

            return polymer.Counter.Max - polymer.Counter.Min;
        }

        public class Counter
        {
            private readonly long[] counter;

            public long Max => counter.Max();
            public long Min => counter.Where(v => v > 0).Min();

            public Counter()
            {
                counter = new long['Z'];
            }

            public long this[int index]
            {
                get
                {
                    if (counter.ElementAtOrDefault(index) == default)
                        counter[index] = 0L;
                    return counter[index];
                }
                set => counter[index] = value;
            }
        }

        public class Polymer
        {
            public readonly Counter Counter = new();
            private readonly Dictionary<Pair, long> pairs = new();

            public Polymer(string startTemplate)
            {
                for (var i = 0; i < startTemplate.Length; i++)
                {
                    if (i < startTemplate.Length - 1)
                        Add(new Pair(startTemplate.Substring(i, 2)), 1);
                    Counter[startTemplate[i]]++;
                }
            }

            public void Add(Pair pair, long amount)
            {
                if (pairs.ContainsKey(pair))
                    pairs[pair] += amount;
                else
                    pairs.Add(pair, amount);
            }

            public void Reinforce()
            {
                var newPairs = new List<(Pair, long)>();
                foreach (var pair in pairs)
                {
                    var rule = rules.FirstOrDefault(r => r.Equals(pair.Key));
                    if (rule != null)
                    {
                        newPairs.Add((rule.NewPair1, pair.Value));
                        newPairs.Add((rule.NewPair2, pair.Value));
                        Counter[rule.NewPair2.OldPair[0]] += pair.Value;
                    }
                    else
                        newPairs.Add((pair.Key, pair.Value));
                }

                pairs.Clear();
                foreach (var pair in newPairs)
                    Add(pair.Item1, pair.Item2);
            }
        }

        public class Pair : IEquatable<Pair>
        {
            public char[] OldPair { get; }
            public Pair NewPair1 { get; }
            public Pair NewPair2 { get; }

            public Pair(string pairInput)
            {
                if(pairInput.Length >= 2)
                    OldPair = new char[2] { pairInput[0], pairInput[1] };
                if (pairInput.Length >= 6)
                {
                    NewPair1 = new Pair(new char[2] { pairInput[0], pairInput[6] });
                    NewPair2 = new Pair(new char[2] { pairInput[6], pairInput[1] });
                }
            }

            public Pair(char[] pair) => OldPair = pair;

            public bool Equals(Pair other) => other is Pair && OldPair[0] == other.OldPair[0] && OldPair[1] == other.OldPair[1];

            public override bool Equals(object obj) => Equals(obj as Pair);

            public override int GetHashCode() => 0;
        }
    }
}