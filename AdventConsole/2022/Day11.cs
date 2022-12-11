namespace AdventCode.Aoc2022;

public class Day11 : IPuzzleDay
{
    public object CalculateAnswerPuzzle1()
    {
        var monkeys = new List<Monkey>
        {
            new Monkey(o => o * 3, n => n / 3, 5, 2, 7, 78, 53, 89, 51, 52, 59, 58, 85),
            new Monkey(o => o + 7, n => n / 3, 2, 3, 6, 64),
            new Monkey(o => o + 5, n => n / 3, 13, 5, 4, 71, 93, 65, 82),
            new Monkey(o => o + 8, n => n / 3, 19, 6, 0, 67, 73, 95, 75, 56, 74),
            new Monkey(o => o + 4, n => n / 3, 11, 3, 1, 85, 91, 90),
            new Monkey(o => o * 2, n => n / 3, 3, 4, 1, 67, 96, 69, 55, 70, 83, 62),
            new Monkey(o => o + 6, n => n / 3, 7, 7, 0, 53, 86, 98, 70, 64),
            new Monkey(o => o * o, n => n / 3, 17, 2, 5, 88, 64)
        };

        return RunAndCalculate(20, monkeys);
    }

    public object CalculateAnswerPuzzle2()
    {
        var monkeys = new List<Monkey>
        {
            new Monkey(o => o * 3, n => n % 9699690, 5, 2, 7, 78, 53, 89, 51, 52, 59, 58, 85),
            new Monkey(o => o + 7, n => n % 9699690, 2, 3, 6, 64),
            new Monkey(o => o + 5, n => n % 9699690, 13, 5, 4, 71, 93, 65, 82),
            new Monkey(o => o + 8, n => n % 9699690, 19, 6, 0, 67, 73, 95, 75, 56, 74),
            new Monkey(o => o + 4, n => n % 9699690, 11, 3, 1, 85, 91, 90),
            new Monkey(o => o * 2, n => n % 9699690, 3, 4, 1, 67, 96, 69, 55, 70, 83, 62),
            new Monkey(o => o + 6, n => n % 9699690, 7, 7, 0, 53, 86, 98, 70, 64),
            new Monkey(o => o * o, n => n % 9699690, 17, 2, 5, 88, 64)
        };

        return RunAndCalculate(10000, monkeys);
    }

    private long RunAndCalculate(int rounds, List<Monkey> monkeys)
    {
        for (var round = 1; round <= rounds; round++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.CanProcessItem())
                {
                    var item = monkey.ProcessItem();
                    monkeys[monkey.ThrowTo].Throw(item);
                }
            }
        }

        var results = monkeys.OrderByDescending(x => x.Inspections).ToArray();
        return results[0].Inspections * results[1].Inspections;
    }

    private class Monkey
    {
        private readonly Queue<long> items = new();
        private readonly Func<long, long> operation1;
        private readonly Func<long, long> operation2;
        private readonly int divisibleBy;
        private readonly int throwToIndexWhenTrue;
        private readonly int throwToIndexWhenFalse;

        public long Inspections { get; private set; }
        public int ThrowTo { get; private set; }

        public Monkey(Func<long, long> operation1, Func<long, long> operation2, int divisibleBy, int throwToIndexWhenTrue, int throwToIndexWhenFalse, params int[] startingItems)
        {
            this.operation1 = operation1;
            this.operation2 = operation2;
            this.divisibleBy = divisibleBy;
            this.throwToIndexWhenTrue = throwToIndexWhenTrue;
            this.throwToIndexWhenFalse = throwToIndexWhenFalse;

            foreach(var item in startingItems)
                items.Enqueue(item);
        }

        public bool CanProcessItem() => items.Any();

        public long ProcessItem()
        {
            Inspections++;

            var newitem = operation1.Invoke(items.Dequeue());
            newitem = operation2.Invoke(newitem);

            ThrowTo = newitem % divisibleBy == 0 ? throwToIndexWhenTrue : throwToIndexWhenFalse;

            return newitem;
        }

        public void Throw(long item) => items.Enqueue(item);
    }
}