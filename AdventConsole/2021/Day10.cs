namespace AdventCode.Aoc2021.Aoc2021;

public class Day10 : IPuzzleDay
{
    private readonly IEnumerable<char[]> input = InputReader.ReadLines().Select(s => s.Select(c => c).ToArray());
    private Chunks chunks;

    public object CalculateAnswerPuzzle1()
    {
        chunks = new()
        {
            new('(', ')', 3),
            new('[', ']', 57),
            new('{', '}', 1197),
            new('<', '>', 25137)
        };

        return input.Sum(l => Parse(l).InvalidChunk?.Score ?? 0);
    }

    public object CalculateAnswerPuzzle2()
    {
        chunks = new()
        {
            new('(', ')', 1),
            new('[', ']', 2),
            new('{', '}', 3),
            new('<', '>', 4)
        };

        var missingCharsScores = new List<long>();
        foreach (var line in input)
        {
            var result = Parse(line);
            if (result.MissingChunks != null)
            {
                var missingCharsScore = 0L;
                foreach (var missingChunk in result.MissingChunks)
                {
                    missingCharsScore *= 5L;
                    missingCharsScore += missingChunk.Score;
                }
                missingCharsScores.Add(missingCharsScore);
            }
        }

        return missingCharsScores.OrderBy(x => x).ToArray()[(missingCharsScores.Count - 1) / 2];
    }

    private ParseResult Parse(IEnumerable<char> line)
    {
        var chunkStack = new Stack<Chunk>();
        foreach (var c in line)
        {
            if (chunks.IsOpenChar(c))
            {
                chunkStack.Push(chunks[c]);
            }
            else
            {
                var lastChunk = chunkStack.Pop();
                if (lastChunk.Close != c)
                {
                    return new()
                    {
                        InvalidChunk = chunks[c]
                    };
                }
            }
        }
        return new()
        {
            MissingChunks = chunkStack.ToArray()
        };
    }

    private struct ParseResult
    {
        public Chunk InvalidChunk { get; set; }
        public Chunk[] MissingChunks { get; set; }
    }

    private class Chunks : List<Chunk>
    {
        public Chunk this[char c] => this.Single(x => x.Open == c || x.Close == c);

        public bool IsOpenChar(char c) => this.Any(x => x.Open == c);
    }

    private class Chunk
    {

        public Chunk(char open, char close, int score)
        {
            Open = open;
            Close = close;
            Score = score;
        }
        public char Open { get; }
        public char Close { get; }
        public int Score { get; }
    }
}
