namespace AdventCode;

public static class InputReader
{
    public static int Year { get; set; }
    public static int Day { get; set; }

    public static string ReadText(string? fileName = null) => File.ReadAllText(@$"{Year}\Inputs\{fileName ?? $"Day{Day}.txt"}");
    public static IEnumerable<string> ReadLines(string? fileName = null) => File.ReadAllLines(@$"{Year}\Inputs\{fileName ?? $"Day{Day}.txt"}");

    public static T[,] ReadGrid<T>(Func<char, T>? conversion = null, string? fileName = null)
    {
        var lines = ReadLines(fileName).ToArray();
        var grid = new T[lines.Length, lines[0].Length];

        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[y].Length; x++)
            {
                grid[y, x] = conversion is not null
                    ? conversion.Invoke(lines[y][x])
                    : (T)Convert.ChangeType(lines[y][x], typeof(T));
            }
        }

        return grid;
    }
}
