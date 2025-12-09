namespace AdventCode.Aoc2023;

public class Day6 : IPuzzleDay
{
    public object CalculateAnswerPuzzle1()
    {
        int[] times = [46, 80, 78, 66];
        int[] distances = [214, 1177, 1402, 1024];
        var results = new int[times.Length];

        for (var raceIndex = 0; raceIndex < times.Length; raceIndex++)
        {
            var sumRace = 0;

            for (var speed = 0; speed < times[raceIndex]; speed++)
            {
                if ((times[raceIndex] - speed) * speed > distances[raceIndex])
                    sumRace++;
            }

            results[raceIndex] = sumRace;
        }

        return results.Aggregate(1, func: (x, y) => x * y);
    }

    public object CalculateAnswerPuzzle2()
    {
        long time = 46807866;
        var distance = 214117714021024;

        var sumRace = 0;

        for (var speed = 0; speed < time; speed++)
        {
            if ((time - speed) * speed > distance)
                sumRace++;
        }

        return sumRace;
    }
}
