namespace AdventCode
{
    public static class InputReader
    {
        public static int Year { get; set; }
        public static int Day { get; set; }

        public static string ReadText(string fileName = null) => File.ReadAllText(@$"{Year}\Inputs\{fileName ?? $"Day{Day}.txt"}");
        public static IEnumerable<string> ReadLines(string fileName = null) => File.ReadAllLines(@$"{Year}\Inputs\{fileName ?? $"Day{Day}.txt"}");
    }
}
