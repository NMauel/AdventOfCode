namespace AdventCode.Aoc2022;

public class Day8 : IPuzzleDay
{
    private readonly char[,] treeMap = InputReader.ReadLines().ToMatrix();

    public object CalculateAnswerPuzzle1()
    {
        var visibilityMap = new bool[treeMap.GetLength(0), treeMap.GetLength(1)];

        //horizontal sweep
        for (var x = 0; x < treeMap.GetLength(0); x++)
        {
            //Left to right
            var highest = 0;
            for (var y = 0; y < treeMap.GetLength(1); y++)
            {
                if (treeMap[y, x] > highest)
                {
                    highest = treeMap[y, x];
                    visibilityMap[y, x] = true;
                }
            }
            //right to left
            highest = 0;
            for (var y = treeMap.GetLength(1) - 1; y >= 0; y--)
            {
                if (treeMap[y, x] > highest)
                {
                    highest = treeMap[y, x];
                    visibilityMap[y, x] = true;
                }
            }
        }
        //vertical sweep
        for (var y = 1; y < treeMap.GetLength(1) - 1; y++)
        {
            //top to bottom
            var highest = 0;
            for (var x = 0; x < treeMap.GetLength(0); x++)
            {
                if (treeMap[y, x] > highest)
                {
                    highest = treeMap[y, x];
                    visibilityMap[y, x] = true;
                }
            }
            //right to left
            highest = 0;
            for (var x = treeMap.GetLength(0) - 1; x >= 0; x--)
            {
                if (treeMap[y, x] > highest)
                {
                    highest = treeMap[y, x];
                    visibilityMap[y, x] = true;
                }
            }
        }
        return visibilityMap.To1DArray().Count(x => x);
    }

    public object CalculateAnswerPuzzle2()
    {
        var highestScenicScore = 0;

        for (var y = 0; y < treeMap.GetLength(1); y++)
        {
            for (var x = 0; x < treeMap.GetLength(0); x++)
            {
                var scenicScore = CalculateScenicScore(x, y);
                if (scenicScore > highestScenicScore)
                    highestScenicScore = scenicScore;
            }
        }

        return highestScenicScore;
    }

    private int CalculateScenicScore(int x, int y)
    {
        var currentHeight = treeMap[y, x];
        var scoreLeft = 0;
        var scoreRight = 0;
        var scoreUp = 0;
        var scoreDown = 0;

        var a = x - 1;
        while (a >= 0)
        {
            scoreLeft++;
            if (treeMap[y, a] >= currentHeight)
                break;
            a--;
        }
        a = x + 1;
        while (a < treeMap.GetLength(0))
        {
            scoreRight++;
            if (treeMap[y, a] >= currentHeight)
                break;
            a++;
        }
        a = y - 1;
        while (a >= 0)
        {
            scoreUp++;
            if (treeMap[a, x] >= currentHeight)
                break;
            a--;
        }
        a = y + 1;
        while (a < treeMap.GetLength(1))
        {
            scoreDown++;
            if (treeMap[a, x] >= currentHeight)
                break;
            a++;
        }

        return scoreLeft * scoreRight * scoreUp * scoreDown;
    }
}
