namespace AdventCode.Aoc2022;

public class Day18 : IPuzzleDay
{
    private readonly HashSet<Cube> cubes = InputReader.ReadLines().Select(l => {
        var i = l.Split(',');
        return new Cube(int.Parse(i[0]), int.Parse(i[1]), int.Parse(i[2]));
    }).ToHashSet();

    public object CalculateAnswerPuzzle1() => cubes.Count * 6 - GetNumberOfAdjacentSides(cubes, cubes);

    public object CalculateAnswerPuzzle2()
    {
        var bounds = new Bounds(cubes.Min(x => x.X) - 1, cubes.Max(x => x.X) + 1,
                                cubes.Min(x => x.Z) - 1, cubes.Max(x => x.Z) + 1,
                                cubes.Min(x => x.Z) - 1, cubes.Max(x => x.Z) + 1);

        var floodSet = new HashSet<Cube>();
        FloodFill3D(floodSet, bounds);

        return GetNumberOfAdjacentSides(floodSet, cubes);
    }

    private void FloodFill3D(HashSet<Cube> floodSet, Bounds bounds)
    {
        var hashSet = new HashSet<Cube>(new[]
        {
            new Cube(bounds.MinX, bounds.MinY, bounds.MinZ)
        });

        while (hashSet.Any())
        {
            var currentCube = hashSet.First();
            hashSet.Remove(currentCube);
            if (!cubes.Contains(currentCube) && floodSet.Add(currentCube))
            {
                if (currentCube.X > bounds.MinX)
                    hashSet.Add(new(currentCube.X - 1, currentCube.Y, currentCube.Z));
                if (currentCube.X < bounds.MaxX)
                    hashSet.Add(new(currentCube.X + 1, currentCube.Y, currentCube.Z));
                if (currentCube.Y > bounds.MinY)
                    hashSet.Add(new(currentCube.X, currentCube.Y - 1, currentCube.Z));
                if (currentCube.Y < bounds.MaxY)
                    hashSet.Add(new(currentCube.X, currentCube.Y + 1, currentCube.Z));
                if (currentCube.Z > bounds.MinZ)
                    hashSet.Add(new(currentCube.X, currentCube.Y, currentCube.Z - 1));
                if (currentCube.Z < bounds.MaxZ)
                    hashSet.Add(new(currentCube.X, currentCube.Y, currentCube.Z + 1));
            }
        }
    }

    private int GetNumberOfAdjacentSides(HashSet<Cube> hashSetLeft, HashSet<Cube> hashSetRight)
    {
        var adjacentSides = 0;
        foreach (var cubeLeft in hashSetLeft)
        {
            adjacentSides += hashSetRight.Count(c => c.X == cubeLeft.X && c.Y == cubeLeft.Y && c.Z == cubeLeft.Z - 1);
            adjacentSides += hashSetRight.Count(c => c.X == cubeLeft.X && c.Y == cubeLeft.Y && c.Z == cubeLeft.Z + 1);
            adjacentSides += hashSetRight.Count(c => c.Y == cubeLeft.Y && c.Z == cubeLeft.Z && c.X == cubeLeft.X - 1);
            adjacentSides += hashSetRight.Count(c => c.Y == cubeLeft.Y && c.Z == cubeLeft.Z && c.X == cubeLeft.X + 1);
            adjacentSides += hashSetRight.Count(c => c.X == cubeLeft.X && c.Z == cubeLeft.Z && c.Y == cubeLeft.Y - 1);
            adjacentSides += hashSetRight.Count(c => c.X == cubeLeft.X && c.Z == cubeLeft.Z && c.Y == cubeLeft.Y + 1);
        }
        return adjacentSides;
    }

    public record Cube(int X, int Y, int Z);

    public record Bounds(int MinX, int MaxX, int MinY, int MaxY, int MinZ, int MaxZ);
}
