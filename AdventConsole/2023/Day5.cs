namespace AdventCode.Aoc2023;

public class Day5 : IPuzzleDay
{
    private readonly Maps maps = InputReader.ReadLines().ParseToMaps();

    private readonly long[] seeds =
    [
        1263068588, 44436703, 1116624626, 2393304, 2098781025,
        128251971, 2946842531, 102775703, 2361566863, 262106125,
        221434439, 24088025, 1368516778, 69719147, 3326254382,
        101094138, 1576631370, 357411492, 3713929839, 154258863
    ];

    public object CalculateAnswerPuzzle1()
    {
        var lowestLocation = long.MaxValue;
        foreach (var seed in seeds)
        {
            lowestLocation = Math.Min(maps.GetSeed(seed), lowestLocation);
        }

        return lowestLocation;
    }

    public object CalculateAnswerPuzzle2()
    {
        //Not the fastest way to find the solution but it works (it took 20 minutes and 39 seconds... :-D)
        var lowestLocation = long.MaxValue;
        for (var i = 0; i < seeds.Length; i += 2)
        for (var seed = seeds[i]; seed < seeds[i] + seeds[i + 1]; seed++)
        {
            lowestLocation = Math.Min(maps.GetSeed(seed), lowestLocation);
        }

        return lowestLocation;
    }
}

public class Maps
{
    private readonly List<Map> maps = [];

    public void AddMap(Map map) => maps.Add(map);

    public long GetSeed(long index)
    {
        foreach (var map in maps)
        {
            index = map[index];
        }
        return index;
    }
}

public class Map
{
    private readonly List<MapRule> rules = [];

    public long this[long index]
    {
        get
        {
            foreach (var rule in rules)
            {
                if (rule.TryGetDestination(index, out var destination))
                    return destination;
            }
            return index;
        }
    }

    public void AddRule(long destination, long source, long length) => rules.Add(new(destination, source, length));
}

public class MapRule(long DestinationRangeStart, long SourceRangeStart, long RangeLength)
{
    public bool TryGetDestination(long source, out long destination)
    {
        destination = -1;

        var sourceOffset = source - SourceRangeStart;

        if (sourceOffset >= 0 && sourceOffset < RangeLength)
            destination = DestinationRangeStart + sourceOffset;

        return destination > -1;
    }
}

public static class Day5Extensions
{
    public static Maps ParseToMaps(this IEnumerable<string> lines)
    {
        Maps maps = new();
        Map map = null;

        foreach (var line in lines)
        {
            map ??= new();
            if (line.Length == 0)
            {
                maps.AddMap(map);
                map = null;
            }
            else
            {
                var data = line.Split(' ').Select(long.Parse).ToArray();
                map.AddRule(data[0], data[1], data[2]);
            }
        }
        if (map != null)
            maps.AddMap(map);

        return maps;
    }
}
