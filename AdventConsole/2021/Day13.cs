namespace AdventCode.Aoc2021
{
    public class Day13 : IPuzzleDay
    {
        public IEnumerable<(int, int)> input = InputReader.ReadLines().Select(line => line.Map<int>(',')).ToArray();

        public object CalculateAnswerPuzzle1()
        {
            var paper = new Paper(input);

            paper.FoldAlongX(655);

            return paper.DotCount;
        }

        public object CalculateAnswerPuzzle2()
        {
            var paper = new Paper(input);

            paper.FoldAlongX(655);
            paper.FoldAlongY(447);
            paper.FoldAlongX(327);
            paper.FoldAlongY(223);
            paper.FoldAlongX(163);
            paper.FoldAlongY(111);
            paper.FoldAlongX(81);
            paper.FoldAlongY(55);
            paper.FoldAlongX(40);
            paper.FoldAlongY(27);
            paper.FoldAlongY(13);
            paper.FoldAlongY(6);

            Console.WriteLine(paper); // <-- ANSWER WRITTEN HERE, NICE TWIST! :-)

            return -1;
        }

        private class Paper
        {
            private bool[][] paper;

            public int DotCount => paper.SelectMany(b => b).Count(b => b);

            public Paper(IEnumerable<(int, int)> dotCoords)
            {
                var xMax = dotCoords.Max(c => c.Item1);
                var yMax = dotCoords.Max(c => c.Item2);
                paper = new bool[yMax + 1][];
                for (var y = 0; y <= yMax; y++)
                    paper[y] = new bool[xMax + 1];

                foreach (var coord in dotCoords)
                    paper[coord.Item2][coord.Item1] = true;
            }

            public void FoldAlongX(int foldX)
            {
                for (var y = 0; y < paper.Length; y++)
                {
                    var delta = 1;
                    while (foldX + delta < paper[y].Length && foldX - delta >= 0)
                    {
                        paper[y][foldX - delta] |= paper[y][foldX + delta];
                        delta++;
                    }
                    Array.Resize(ref paper[y], foldX);
                }
            }

            public void FoldAlongY(int foldY)
            {
                var delta = 1;
                while (foldY + delta < paper.Length && foldY - delta >= 0)
                {
                    for (var x = 0; x < paper[0].Length; x++)
                        paper[foldY - delta][x] |= paper[foldY + delta][x];
                    delta++;
                }
                Array.Resize(ref paper, foldY);
            }

            public override string ToString()
            {
                var toString = Environment.NewLine;
                for (var y = 0; y < paper.Length; y++)
                {
                    for (var x = 0; x < paper[0].Length; x++)
                        toString += paper[y][x] ? '#' : ' ';
                    toString += Environment.NewLine;
                }
                return toString;
            }
        }
    }
}