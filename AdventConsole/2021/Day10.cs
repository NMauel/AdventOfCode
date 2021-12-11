using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day10 : IPuzzleDay<long>
    {
        private readonly IEnumerable<char[]> input = InputReader.ReadLines("Day10.txt").Select(s => s.Select(c => c).ToArray());
        private Chunks chunks;

        public long CalculateAnswerPuzzle1()
        {
            chunks = new Chunks {
                new Chunk('(', ')', 3),
                new Chunk('[', ']', 57),
                new Chunk('{', '}', 1197),
                new Chunk('<', '>', 25137)};

            return input.Sum(l => Parse(l).InvalidChunk?.Score ?? 0);
        }

        public long CalculateAnswerPuzzle2()
        {
            chunks = new Chunks {
                new Chunk('(', ')', 1),
                new Chunk('[', ']', 2),
                new Chunk('{', '}', 3),
                new Chunk('<', '>', 4)};

            var missingCharsScores = new List<long>();
            foreach (var line in input)
            {
                var result = Parse(line);
                if (result.MissingChunks != null)
                {
                    var missingCharsScore = 0L;
                    foreach(var missingChunk in result.MissingChunks)
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
                        return new ParseResult { InvalidChunk = chunks[c] };
                    }
                }
            }
            return new ParseResult { MissingChunks = chunkStack.ToArray() };
        }

        private struct ParseResult
        {
            public Chunk InvalidChunk { get; set; }
            public Chunk[] MissingChunks { get; set; }
        }

        private class Chunks : List<Chunk>
        {
            public bool IsOpenChar(char c) => this.Any(x => x.Open == c);
            public Chunk this[char c] => this.Single(x => x.Open == c || x.Close == c);
        }

        private class Chunk
        {
            public char Open { get; }
            public char Close { get; }
            public int Score { get; }

            public Chunk(char open, char close, int score)
            {
                Open = open;
                Close = close;
                Score = score;
            }
        }
    }
}