using Priority_Queue;

namespace AdventCode.Aoc2021
{
    public class Day15 : IPuzzleDay
    {
        private readonly IEnumerable<IEnumerable<int>> input = InputReader.ReadLines().Select(line => line.Select(c => Convert.ToInt32(c - 48)));

        public object CalculateAnswerPuzzle1()
        {
            var cave = new Cave(input.ToMatrix());
            var destinationNode = FindSafestPath(cave, cave.Start, cave.End);
            return destinationNode.TentativeRiskValue - cave.Start.RiskValue;
        }

        public object CalculateAnswerPuzzle2()
        {
            var cave = CreateEnlargedCave();
            var destinationNode = FindSafestPath(cave, cave.Start, cave.End);
            return destinationNode.TentativeRiskValue - cave.Start.RiskValue;
        }

        private Vertex FindSafestPath(Cave cave, Vertex startNode, Vertex destinationNode)
        {
            var priorityQueue = new SimplePriorityQueue<Vertex, int>(); // StablePriorityQueue<Vertex>(20);

            startNode.TentativeRiskValue = startNode.RiskValue;
            startNode.IsVisited = true;
            priorityQueue.Enqueue(startNode, startNode.TentativeRiskValue);

            while (priorityQueue.Any())
            {
                var currentNode = priorityQueue.Dequeue();
                currentNode.IsVisited = true;

                if (currentNode == destinationNode)
                    return currentNode;

                var unvisitedNeighbours = cave.GetUnvisitedNeighbourVertices(currentNode);
                foreach (var vertex in unvisitedNeighbours)
                {
                    if (currentNode.TentativeRiskValue + vertex.RiskValue < vertex.TentativeRiskValue)
                    {
                        vertex.TentativeRiskValue = currentNode.TentativeRiskValue + vertex.RiskValue;
                        vertex.Parent = currentNode;
                        priorityQueue.Enqueue(vertex, vertex.TentativeRiskValue);
                    }
                }
            }
            return null;
        }

        private Cave CreateEnlargedCave()
        {
            var resizedInput = new List<int[]>();
            for (var i = 0; i < 5; i++)
                foreach (var row in input)
                    resizedInput.Add(
                        row.Select(c => ((c + i - 1) % 9) + 1)
                        .Concat(row.Select(c => ((c + i) % 9) + 1))
                        .Concat(row.Select(c => ((c + i + 1) % 9) + 1))
                        .Concat(row.Select(c => ((c + i + 2) % 9) + 1))
                        .Concat(row.Select(c => ((c + i + 3) % 9) + 1))
                        .ToArray());

            return new Cave(resizedInput.ToMatrix());
        }

        private class Cave
        {
            public Vertex[,] Vertices { get; }

            public Cave (int[,] vertices)
            {
                Vertices = new Vertex[vertices.GetLength(0), vertices.GetLength(1)];
                for (var y = 0; y < vertices.GetLength(0); y++)
                    for (var x = 0; x < vertices.GetLength(1); x++)
                        Vertices[y, x] = new Vertex(vertices[y, x], x, y);
            }

            public Vertex Start => Vertices[0, 0];

            public Vertex End => Vertices[Vertices.GetLength(0) - 1, Vertices.GetLength(1) - 1];

            public Vertex this[int y, int x] => Vertices[y, x];

            public IEnumerable<Vertex> GetUnvisitedNeighbourVertices(Vertex vertex) => GetNeighbourVertices(vertex).Where(vertex => !vertex.IsVisited);

            private IEnumerable<Vertex> GetNeighbourVertices(Vertex vertex)
            {
                if (vertex.X < Vertices.GetLength(1) - 1) yield return Vertices[vertex.Y, vertex.X + 1];
                if (vertex.Y < Vertices.GetLength(0) - 1) yield return Vertices[vertex.Y + 1, vertex.X];
                if (vertex.X > 0) yield return Vertices[vertex.Y, vertex.X - 1];
                if (vertex.Y > 0) yield return Vertices[vertex.Y - 1, vertex.X];
            }
        }

        public class Vertex
        {
            public int X { get; }
            public int Y { get; }

            public int RiskValue { get; }
            public int TentativeRiskValue { get; set; } = int.MaxValue;
            public bool IsVisited { get; set; } = false;
            public Vertex Parent { get; set; } = null;

            public Vertex(int riskValue, int x, int y)
            {
                RiskValue = riskValue;
                X = x;
                Y = y;
            }
        }
    }
}