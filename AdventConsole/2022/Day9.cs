using static AdventCode.Aoc2022.Day9;

namespace AdventCode.Aoc2022;

public class Day9 : IPuzzleDay
{
    private readonly IEnumerable<(char Direction, int Distance)> moves = InputReader.ReadLines().Select(x => (x[0], int.Parse(x.Substring(2))));

    public object CalculateAnswerPuzzle1() => new Rope(2).Transform(moves).TailHistory.Distinct().Count();

    public object CalculateAnswerPuzzle2() => new Rope(10).Transform(moves).TailHistory.Distinct().Count();

    private class Rope
    {
        private (int x, int y)[] knots;
        public List<(int x, int y)> TailHistory { get; } = new();

        public Rope(int length)
        {
            knots = Enumerable.Repeat((0, 0), length).ToArray();
            TailHistory.Add((0, 0));
        }

        public Rope Transform(IEnumerable<(char Direction, int Distance)> moves)
        {
            foreach (var move in moves)
            {
                for (var x = 0; x < move.Distance; x++)
                {
                    switch (move.Direction)
                    {
                        case 'R': knots[0].x++; break;
                        case 'L': knots[0].x--; break;
                        case 'U': knots[0].y++; break;
                        case 'D': knots[0].y--; break;
                    }

                    for (var p = 1; p < knots.Length; p++)
                        Follow(p);

                    if (TailHistory.Last() != knots.Last())
                        TailHistory.Add(knots.Last());
                }
            }
            return this;
        }

        private void Follow(int knotIndex)
        {
            var horizontalDistance = knots[knotIndex - 1].x - knots[knotIndex].x;
            var verticalDistance = knots[knotIndex - 1].y - knots[knotIndex].y;

            if (horizontalDistance > 1) // R
            {
                knots[knotIndex].x++;
                if (verticalDistance > 0) // U
                    knots[knotIndex].y++;
                if (verticalDistance < 0) // D
                    knots[knotIndex].y--;
            }
            else if (horizontalDistance < -1) // L
            {
                knots[knotIndex].x--;
                if (verticalDistance > 0) // U
                    knots[knotIndex].y++;
                if (verticalDistance < 0) // D
                    knots[knotIndex].y--;
            }
            else if (verticalDistance > 1) //U
            {
                knots[knotIndex].y++;
                if (horizontalDistance > 0) // R
                    knots[knotIndex].x++;
                if (horizontalDistance < 0) // L
                    knots[knotIndex].x--;
            }
            else if (verticalDistance < -1) // D
            {
                knots[knotIndex].y--;
                if (horizontalDistance > 0) // R
                    knots[knotIndex].x++;
                if (horizontalDistance < 0) // L
                    knots[knotIndex].x--;
            }
        }
    }
}