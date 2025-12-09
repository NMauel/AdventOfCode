namespace AdventCode.Aoc2021;

public class Day2
{
    private static readonly IEnumerable<(string, int)> input = InputReader.ReadLines()
        .Select(line => {
            var splitted = line.Split(' ');
            return (splitted[0], Convert.ToInt32(splitted[1]));
        });

    public class Puzzle1 : IPuzzleDay
    {
        public object CalculateAnswerPuzzle1()
        {
            var length = input.Where(line => line.Item1 == "forward").Sum(line => line.Item2);
            var depth = input.Where(line => line.Item1 == "down").Sum(line => line.Item2);
            depth -= input.Where(line => line.Item1 == "up").Sum(line => line.Item2);

            return length * depth;
        }

        public object CalculateAnswerPuzzle2()
        {
            int length = 0, depth = 0, aim = 0;

            foreach (var line in input)
            {
                switch (line.Item1)
                {
                    case "up":
                        aim -= line.Item2;
                        break;
                    case "down":
                        aim += line.Item2;
                        break;
                    case "forward":
                        length += line.Item2;
                        depth += aim * line.Item2;
                        break;
                }
            }

            return length * depth;
        }
    }
}
