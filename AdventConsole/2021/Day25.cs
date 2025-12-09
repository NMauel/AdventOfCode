namespace AdventCode.Aoc2021;

public class Day25 : IPuzzleDay
{
    private readonly char[][] input = InputReader.ReadLines().Select(line => line.ToCharArray()).ToArray();

    public object CalculateAnswerPuzzle1()
    {
        var map = new Map(input);
        var iterations = 1;

        Console.WriteLine(map);

        while (Iterate(map))
        {
            iterations++;
            Console.WriteLine($"-- After iteration {iterations} --");
            Console.WriteLine(map);
        }

        return iterations;
    }

    public object CalculateAnswerPuzzle2() => 0;

    public bool Iterate(Map map)
    {
        var hasMoved = false;
        var oldMap = map.Clone();

        for (var r = 0; r < map.Height; r++)
        {
            for (var c = map.Width - 1; c >= 0; c--)
            {
                if (oldMap[r, c] == '>' && oldMap[r, c + 1] == '.')
                {
                    map[r, c] = '.';
                    map[r, c + 1] = '>';
                    hasMoved |= true;
                }
            }
        }
        oldMap = map.Clone();
        for (var c = 0; c < map.Width; c++)
        {
            for (var r = map.Height - 1; r >= 0; r--)
            {
                if (oldMap[r, c] == 'v' && oldMap[r + 1, c] == '.')
                {
                    map[r, c] = '.';
                    map[r + 1, c] = 'v';
                    hasMoved |= true;
                }
            }
        }

        return hasMoved;
    }

    public class Map
    {
        private readonly char[][] map;

        public Map(int width, int height)
        {
            map = new char[height][];
            for (var i = 0; i < height; i++)
            {
                map[i] = new char[width];
                Array.Fill(map[i], '.');
            }
        }

        public Map(char[][] map)
        {
            this.map = map;
        }

        public int Width => map.First().Length;

        public int Height => map.Length;

        public char this[int row, int column]
        {
            get => map[row % Height][column % Width];
            set => map[row % Height][column % Width] = value;
        }

        public override string ToString()
        {
            return string.Join("\r\n", map.Select(line => new string(line)));
        }

        public Map Clone()
        {
            var newmap = new char[Height][];
            for (var i = 0; i < Height; i++)
            {
                newmap[i] = new char[Width];
                Array.Copy(map[i], newmap[i], map[i].Length);
            }
            return new(newmap);
        }
    }
}
