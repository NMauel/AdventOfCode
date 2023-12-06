namespace AdventCode.Aoc2023;

public class Day6 : IPuzzleDay
{
    public object CalculateAnswerPuzzle1()
    {
        int[] times = [46, 80, 78, 66];
        int[] distances = [214, 1177, 1402, 1024];
        int[] results = new int[times.Length];

        for (int raceIndex = 0; raceIndex < times.Length; raceIndex++)
        {
            int sumRace = 0;

            for (int speed = 0; speed < times[raceIndex]; speed++)
                if((times[raceIndex] - speed) * speed > distances[raceIndex])
                    sumRace++;

            results[raceIndex] = sumRace;
        }

        return results.Aggregate(1, (x, y) => x * y);
    }

    public object CalculateAnswerPuzzle2()
    {
        long time = 46807866;
        long distance = 214117714021024;

        int sumRace = 0;

        for (int speed = 0; speed < time; speed++)
            if ((time - speed) * speed > distance)
                sumRace++;

        return sumRace;
    }
}