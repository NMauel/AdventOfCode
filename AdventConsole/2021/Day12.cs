namespace AdventCode.Aoc2021
{
    public class Day12 : IPuzzleDay
    {
        private readonly IEnumerable<Cave> input = ReadCaves();

        public object CalculateAnswerPuzzle1()
        {
            var foundRoutes = new HashSet<string>();
            VisitNextCave(input.First(c => c.Id == "start"), ref foundRoutes, new Stack<Cave>());
            return foundRoutes.Count;

            void VisitNextCave(Cave currentCave, ref HashSet<string> foundRoutes, Stack<Cave> traversedRoute)
            {
                traversedRoute.Push(currentCave);
                if (currentCave.Id == "end")
                    foundRoutes.Add(string.Join(',', traversedRoute.Select(r => r.Id).ToArray()));
                else
                    foreach (var nextCave in currentCave.Paths.Where(p => p.IsLargeCave || !traversedRoute.Contains(p)))
                        VisitNextCave(nextCave, ref foundRoutes, traversedRoute);
                traversedRoute.Pop();
            }
        }

        public object CalculateAnswerPuzzle2()
        {
            var foundRoutes = new HashSet<string>();
            foreach (var smallCaveToVisitTwice in input.Where(c => !c.IsLargeCave && c.Id != "start" && c.Id != "end"))
                VisitNextCave(input.First(c => c.Id == "start"), ref foundRoutes, new Stack<Cave>(), smallCaveToVisitTwice);
            return foundRoutes.Count;

            void VisitNextCave(Cave currentCave, ref HashSet<string> foundRoutes, Stack<Cave> traversedRoute, Cave smallCaveToVisitTwice)
            {
                traversedRoute.Push(currentCave);
                if (currentCave.Id == "end")
                    foundRoutes.Add(string.Join(',', traversedRoute.Select(r => r.Id).ToArray()));
                else
                    foreach (var nextCave in currentCave.Paths.Where(p => p.IsLargeCave || (p.Id != "start" && traversedRoute.Count(r => r == p) < (p == smallCaveToVisitTwice ? 2 : 1))))
                        VisitNextCave(nextCave, ref foundRoutes, traversedRoute, smallCaveToVisitTwice);
                traversedRoute.Pop();
            }
        }

        private static IEnumerable<Cave> ReadCaves()
        {
            var caves = new List<Cave>();
            foreach (var line in InputReader.ReadLines())
            {
                var caveIds = line.Split('-');
                var caveA = GetCave(caveIds.First());
                var caveB = GetCave(caveIds.Last());

                caveA.Paths.Add(caveB);
                caveB.Paths.Add(caveA);
            }
            return caves;

            Cave GetCave(string id)
            {
                var cave = caves.FirstOrDefault(x => x.Id == id);
                if (cave == null)
                    caves.Add(cave = new Cave(id));
                return cave;
            }
        }

        private class Cave : IEquatable<Cave>
        {
            public string Id { get; }
            public bool IsLargeCave => Id.All(c => char.IsUpper(c));

            public HashSet<Cave> Paths { get; } = new HashSet<Cave>();

            public Cave(string id)
            {
                Id = id;                
            }

            public bool Equals(Cave other) => Id.Equals(other.Id);
        }
    }
}