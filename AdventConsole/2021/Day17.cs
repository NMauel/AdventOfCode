namespace AdventCode.Aoc2021
{
    public class Day17 : IPuzzleDay
    {
        private TargetArea target = new(111, 161, -154, -101);

        private IEnumerable<Path> pathsWithinTarget => GetPossiblePathsWithinTarget();

        public object CalculateAnswerPuzzle1() => pathsWithinTarget.SelectMany(p => p.Points).Max(p => p.Y);

        public object CalculateAnswerPuzzle2() => pathsWithinTarget.Count();

        private IEnumerable<Path> GetPossiblePathsWithinTarget()
        {
            int xvelocity = 0;
            int yvelocity = target.Ymin;
            int overshoot = 0;
            List<Path> paths = new();

            while (overshoot < 500)
            {
                var path = FireProbe(xvelocity, yvelocity);
                switch (path.Result)
                {
                    case TargetResult.Within:
                        paths.Add(path);
                        break;
                    case TargetResult.Over:
                        if (xvelocity > target.Xmax)
                        {
                            overshoot++;
                            yvelocity++;
                            xvelocity = -1;
                        }
                        break;
                }
                xvelocity++;
            }
            return paths;
        }

        private Path FireProbe(int xvelocity, int yvelocity)
        {
            var path = new Path();
            int x = 0, y = 0;

            while (path.Result == TargetResult.Unknown)
            {
                x += xvelocity;
                y += yvelocity;
                if (xvelocity > 0) xvelocity--;
                if (xvelocity < 0) xvelocity++;
                yvelocity--;

                path.AddPoint(x, y);
                path.Result = target.GetProbeResult(x, y);                
            }
            return path;
        }

        private class TargetArea
        {
            public int Xmin { get; }
            public int Xmax { get; }
            public int Ymin { get; }
            public int Ymax { get; }

            public TargetArea(int xmin, int xmax, int ymin, int ymax)
            {
                Xmin = xmin;
                Xmax = xmax;
                Ymin = ymin;
                Ymax = ymax;
            }

            public TargetResult GetProbeResult(int x, int y)
            {
                if (x > Xmax)
                    return TargetResult.Over;
                if (y < Ymin)
                    return TargetResult.Under;
                if (x >= Xmin && y <= Ymax)
                    return TargetResult.Within;
                return TargetResult.Unknown;
            }
        }

        private class Path
        {
            private readonly List<Point> points = new();

            public IEnumerable<Point> Points => points.AsReadOnly();
            public TargetResult Result { get; set; }

            public void AddPoint(int x, int y)
            {
                points.Add(new Point(x, y));
            }
        }

        private class Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public enum TargetResult
        {
            Unknown,
            Within,
            Under,
            Over
        }
    }
}