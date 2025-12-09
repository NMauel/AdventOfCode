namespace AdventCode.Aoc2021;

public class Day4 : IPuzzleDay
{
    private readonly BingoCard[] cards = InputReader.ReadText("Day4_A.txt").Split("\r\n\r\n")
        .Select(card => new BingoCard(card.Split("\r\n")
                                          .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(int.Parse)))).ToArray();
    private readonly IEnumerable<int> numbers = InputReader.ReadText("Day4_B.txt").Split(',').Select(int.Parse);

    public object CalculateAnswerPuzzle1()
    {
        //Process all numbers
        foreach (var number in numbers)
        {
            foreach (var card in cards)
            {
                card.MarkField(number);
                if (card.HasBingo())
                    return card.Fields.SelectMany(rows => rows).Where(field => !field.IsMarked).Sum(field => field.Number) * number;
            }
        }
        throw new NotSupportedException("Answer not found!");
    }

    public object CalculateAnswerPuzzle2()
    {
        //Process all numbers
        foreach (var number in numbers)
        {
            foreach (var card in cards)
            {
                card.MarkField(number);
                if (card.HasBingo() && cards.All(card => card.HasBingo()))
                    return card.Fields.SelectMany(rows => rows).Where(field => !field.IsMarked).Sum(field => field.Number) * number;
            }
        }
        throw new NotSupportedException("Answer not found!");
    }

    private class BingoCard
    {

        public BingoCard(IEnumerable<IEnumerable<int>> card)
        {
            Fields = card.Select(rows => rows.Select(number => new BingoField(number)).ToArray()).ToArray();
        }
        public BingoField[][] Fields { get; }

        public void MarkField(int number) =>
            Fields.SelectMany(fields => fields).FirstOrDefault(field => field.Number == number)?.Mark();

        public bool HasBingo()
        {
            //Has a horizontal bingo
            if (Fields.Any(row => row.All(field => field.IsMarked)))
                return true;

            //Has a vertical bingo
            for (var column = 0; column < Fields.First().Count(); column++)
            {
                if (Fields.All(rows => rows[column].IsMarked))
                    return true;
            }

            return false;
        }
    }

    private class BingoField
    {

        public BingoField(int number)
        {
            Number = number;
        }
        public int Number { get; }
        public bool IsMarked { get; private set; }

        public void Mark() => IsMarked = true;
    }
}
