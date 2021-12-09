using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day9 : IPuzzleDay<int>
    {
        private readonly int[][] heightmap = InputReader.ReadLines("Day9.txt")
            .Select(line => line.Select(c => Convert.ToInt32(c) - 48).ToArray()).ToArray();

        public int CalculateAnswerPuzzle1()
        {
            int valleyCount = 0;
            for(var y = 0; y < heightmap.Length; y++)
                for(var x = 0; x < heightmap[y].Length; x++)
                    if (IsCenterOfValley(x, y))
                        valleyCount += heightmap[y][x] + 1;
            return valleyCount;
        }

        private bool IsCenterOfValley(int x, int y)
        {
            return (y == 0 || heightmap[y][x] < heightmap[y - 1][x])
                && (y == heightmap.Length - 1 || heightmap[y][x] < heightmap[y + 1][x])
                && (x == 0 || heightmap[y][x] < heightmap[y][x - 1])
                && (x == heightmap[y].Length - 1 || heightmap[y][x] < heightmap[y][x + 1]);
        }

        public int CalculateAnswerPuzzle2()
        {
            var clustermap = heightmap.Select(r => r.Select(c => c < 9 ? 0 : -1).ToArray()).ToArray();

            var currentIndex = 1;
            for (var y = 0; y < clustermap.Length; y++)
                for (var x = 0; x < clustermap[y].Length; x++)
                    IndexClusterUsingBFS(clustermap, currentIndex++, x, y);

            var clusterSizes = new List<int>();
            for (var i = 1; i <= currentIndex - 1; i++)
                clusterSizes.Add(clustermap.SelectMany(r => r).Count(c => c == i));
            return clusterSizes.OrderByDescending(c => c).Take(3).Multiply();
        }

        private void IndexClusterUsingBFS(int[][] clustermap, int index, int x, int y)
        {
            if (clustermap[y][x] != 0)
                return;

            clustermap[y][x] = index;
            for (var dx = x - 1; dx <= x + 1; dx++)
                if (dx >= 0 && dx < clustermap[y].Length && clustermap[y][dx] == 0)
                    IndexClusterUsingBFS(clustermap, index, dx, y);
            for (var dy = y - 1; dy <= y + 1; dy++)
                if (dy >= 0 && dy < clustermap.Length && clustermap[dy][x] == 0)
                    IndexClusterUsingBFS(clustermap, index, x, dy);
        }
    }
}