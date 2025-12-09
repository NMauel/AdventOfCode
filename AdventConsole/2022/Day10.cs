namespace AdventCode.Aoc2022;

public class Day10 : IPuzzleDay
{
    private readonly IEnumerable<string> commands = InputReader.ReadLines();

    public object CalculateAnswerPuzzle1() => new GPU(commands).Output;

    public object CalculateAnswerPuzzle2() => new GPU(commands).RenderCRT().Output;

    private class GPU
    {
        private readonly bool[,] crt = new bool[40, 6];

        protected readonly int registerX = 1;
        protected int cycleCount = -1;

        public GPU(IEnumerable<string> commands)
        {
            foreach (var command in commands)
            {
                if (command == "noop")
                {
                    RunCycle();
                }
                else
                {
                    RunCycle();
                    RunCycle();
                    registerX += int.Parse(command.Split(' ')[1]);
                }
            }
        }

        public int Output { get; private set; }

        public GPU RenderCRT()
        {
            for (var y = 0; y < 6; y++)
            {
                for (var x = 0; x < 40; x++)
                {
                    Console.Write(crt[x, y] ? '#' : '.');
                }
                Console.WriteLine();
            }
            return this;
        }

        private void RunCycle()
        {
            cycleCount++;

            if ((cycleCount + 20) % 40 == 0)
            {
                Output += cycleCount * registerX;
            }

            crt[cycleCount % 40, cycleCount / 40] |= cycleCount % 40 >= registerX - 1 && cycleCount % 40 <= registerX + 1;
        }
    }
}
