namespace AdventCode.Aoc2021
{
    public class Day8 : IPuzzleDay
    {
        private readonly IEnumerable<string> input = InputReader.ReadLines();

        public object CalculateAnswerPuzzle1() => 
            input.Select(line => line.Split('|', StringSplitOptions.RemoveEmptyEntries))
                .Select(part => part[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(output => output.Count(segment => segment.Length == 2 || segment.Length == 3 || segment.Length == 4 || segment.Length == 7))
                .Sum();

        public object CalculateAnswerPuzzle2()
        {
            int totalSum = 0;
            var segment = new Segment();
            
            foreach(var line in input)
            {
                var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                var learnSegments = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.OrderBy(s => s).ToArray()).OrderBy(x => x.Length);
                var outputSegments = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.OrderBy(s => s).ToArray());

                segment.Learn(learnSegments);
                totalSum += segment.GetOutput(outputSegments);
            }

            return totalSum;
        }

        private class Segment
        {
            private readonly Dictionary<int, char[]> patterns = new();

            public void Learn(IEnumerable<char[]> input)
            {
                patterns.Clear();
                foreach(var pattern in input)
                {
                    switch (pattern.Length)
                    {
                        case 2:
                            patterns.Add(1, pattern);
                            break;
                        case 3:
                            patterns.Add(7, pattern);
                            break;
                        case 4:
                            patterns.Add(4, pattern);
                            break;
                        case 7:
                            patterns.Add(8, pattern);
                            break;
                        case 6:
                            if (patterns[4].All(c => pattern.Contains(c)))
                                patterns.Add(9, pattern);
                            else if (patterns[7].All(c => pattern.Contains(c)))
                                patterns.Add(0, pattern);
                            else
                                patterns.Add(6, pattern);
                            break;
                        case 5:
                            if (patterns[7].All(c => pattern.Contains(c)))
                                patterns.Add(3, pattern);
                            else if (patterns[4].Count(c => pattern.Contains(c)) == 3)
                                patterns.Add(5, pattern);
                            else
                                patterns.Add(2, pattern);
                            break;
                        default:
                            throw new ArgumentException("Should never happen! :-)");
                    }
                }
            }

            public int GetOutput(IEnumerable<char[]> output)
            {
                int multiplier = 1, number = 0;

                foreach (var o in output.Select(x => x.OrderBy(c => c)).Reverse())
                {
                    number += multiplier * patterns.First(pattern => pattern.Value.SequenceEqual(o)).Key;
                    multiplier *= 10;
                }
                return number;
            }
        }
    }
}