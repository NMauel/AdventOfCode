namespace AdventCode.Aoc2022;

public class Day12 : IPuzzleDay
{
    private const char startChar = (char)('a' - 1);
    private const char finishChar = (char)('z' + 1);

    public object CalculateAnswerPuzzle1()
    {
        var map = GetMap();
        var startLocation = map.To1DArray().First(l => l.Height == startChar);

        var fewestSteps = BreadthSearchFirst(startLocation, map);
        return fewestSteps > -1 ? fewestSteps : "Error, no path from S to E found!";
    }

    public object CalculateAnswerPuzzle2()
    {
        var possibleStartLocations = GetMap().To1DArray().Where(l => l.Height <= 'a');
        var shortestRoute = int.MaxValue;

        foreach (var startLocation in possibleStartLocations)
        {
            var result = BreadthSearchFirst(startLocation, GetMap());
            if (result > -1 && result < shortestRoute)
                shortestRoute = result;
        }

        return shortestRoute < int.MaxValue ? shortestRoute : "Error, no path from S to any a found!";
    }

    private int BreadthSearchFirst(Location startLocation, Location[,] map)
    {
        var queue = new Queue<Location>();

        startLocation.Steps = 0;
        queue.Enqueue(startLocation);

        while (queue.TryDequeue(out var location))
        {
            var currentSteps = location.Steps;

            foreach (var neighbour in GetNextLocations(location, map).Where(l => l.Height <= location.Height + 1))
            {
                if (neighbour.Steps == -1)
                {
                    neighbour.Steps = currentSteps + 1;
                    queue.Enqueue(neighbour);

                    if (neighbour.Height == finishChar)
                        return currentSteps + 1;
                }
            }
        }

        return -1;
    }

    private Location[,] GetMap() => InputReader.ReadLines().Select(x => x.Replace('S', startChar).Replace('E', finishChar)).ToMatrix((x, y, v) => new Location(x, y, v));

    private IEnumerable<Location> GetNextLocations(Location currentLocation, Location[,] map)
    {
        var neighbourLocations = new List<Location>();

        if (currentLocation.X > 0)
            neighbourLocations.Add(map[currentLocation.Y, currentLocation.X - 1]);
        if (currentLocation.Y > 0)
            neighbourLocations.Add(map[currentLocation.Y - 1, currentLocation.X]);
        if (currentLocation.X < map.GetLength(1) - 1)
            neighbourLocations.Add(map[currentLocation.Y, currentLocation.X + 1]);
        if (currentLocation.Y < map.GetLength(0) - 1)
            neighbourLocations.Add(map[currentLocation.Y + 1, currentLocation.X]);

        return neighbourLocations;
    }

    private class Location
    {

        public Location(int x, int y, char height)
        {
            X = x;
            Y = y;
            Height = height;
        }
        public int X { get; }
        public int Y { get; }
        public char Height { get; }
        public int Steps { get; set; } = -1;
    }
}
