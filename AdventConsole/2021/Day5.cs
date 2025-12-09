namespace AdventCode.Aoc2021;

public class Day5 : IPuzzleDay
{
    public object CalculateAnswerPuzzle1()
    {
        var vents = InputReader.ReadLines().Select(line => {
            var p = line.Replace(" -> ", ",").Split(',').Select(int.Parse).ToArray();
            return GenerateVent(p[0], p[1], p[2], p[3], false);
        }).OfType<Vent>().ToArray();

        return vents.SelectMany(coords => coords).GroupBy(coords => coords.ToString()).Count(groups => groups.Count() > 1);
    }

    public object CalculateAnswerPuzzle2()
    {
        var vents = InputReader.ReadLines().Select(line => {
            var p = line.Replace(" -> ", ",").Split(',').Select(int.Parse).ToArray();
            return GenerateVent(p[0], p[1], p[2], p[3]);
        }).OfType<Vent>().ToArray();

        return vents.SelectMany(coords => coords).GroupBy(coords => coords.ToString()).Count(groups => groups.Count() > 1);
    }

    private Vent GenerateVent(int x1, int y1, int x2, int y2, bool includeDiagonalVents = true)
    {
        var horSteps = x1 <= x2 ? Enumerable.Range(x1, x2 - x1 + 1) : Enumerable.Range(x2, x1 - x2 + 1).Reverse();
        var verSteps = y1 <= y2 ? Enumerable.Range(y1, y2 - y1 + 1) : Enumerable.Range(y2, y1 - y2 + 1).Reverse();

        if (horSteps.Count() == 1 || verSteps.Count() == 1)
            return new(horSteps.SelectMany(h => verSteps.Select(v => new Coordinate(h, v))));

        return includeDiagonalVents ? new Vent(horSteps.Zip(verSteps, resultSelector: (h, v) => new Coordinate(h, v))) : null;
    }

    public class Coordinate
    {

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }

        public override string ToString() => $"({X},{Y})";
    }

    public class Vent : List<Coordinate>
    {
        public Vent(IEnumerable<Coordinate> coordinates)
        {
            AddRange(coordinates);
        }

        public override string ToString() => string.Join(' ', this);
    }
}
