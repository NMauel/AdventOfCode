namespace AdventCode.Aoc2023;

public class Day7 : IPuzzleDay
{
    private readonly SortedList<CamelCardHand, int> hands = InputReader.ReadLines().ParseToCamelCardHands<CamelCardHand>();
    private readonly SortedList<CamelCardHandWithJokers, int> handsWithJokers = InputReader.ReadLines().ParseToCamelCardHands<CamelCardHandWithJokers>();

    public object CalculateAnswerPuzzle1() => CalculateTotalWinnings(hands);

    public object CalculateAnswerPuzzle2() => CalculateTotalWinnings(handsWithJokers);

    private static int CalculateTotalWinnings<T>(SortedList<T, int> hands) where T : CamelCardHand
    {
        int sum = 0;
        int multiplier = 1;

        foreach(var card in hands)
            sum += card.Value * multiplier++;

        return sum;
    }
}

public class CamelCardHand : IComparable<CamelCardHand>
{
    public int[] Hand { get; }
    public int HandType { get; protected set; }

    public CamelCardHand(char[] hand)
    {
        Hand = hand.Select(card => card <= '9' ? card - 48 : SymbolsLookup[card]).ToArray();
        CalculateHandType();
    }

    protected virtual void CalculateHandType()
    {
        var type = (from c in Hand
                    orderby c descending
                    group c by c into t
                    let count = t.Count()
                    orderby count descending
                    select count).ToArray();

        if (type[0] == 5)
            HandType = 6;
        else if (type[0] == 4)
            HandType = 5;
        else if (type[0] == 3 && type[1] == 2)
            HandType = 4;
        else if (type[0] == 3)
            HandType = 3;
        else if (type[0] == 2 && type[1] == 2)
            HandType = 2;
        else if (type[0] == 2)
            HandType = 1;
    }

    public int CompareTo(CamelCardHand otherCardHand)
    {
        if (Hand == null || otherCardHand.Hand == null)
            return 0;

        if(HandType == otherCardHand.HandType)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (Hand[i] == otherCardHand.Hand[i])
                    continue;
                return Hand[i] - otherCardHand.Hand[i];
            }
        }
        return HandType - otherCardHand.HandType;
    }

    protected virtual Dictionary<char, int> SymbolsLookup { get; } = new() { { 'T', 10 }, { 'J', 11 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 } };
}

public class CamelCardHandWithJokers : CamelCardHand
{
    public CamelCardHandWithJokers(char[] hand) : base(hand) { }

    protected override void CalculateHandType()
    {
        var jokers = Hand.Count(x => x == 1);
        var rest = Hand.Where(x => x != 1);

        var type = (from c in rest
                    orderby c descending
                    group c by c into t
                    let count = t.Count()
                    orderby count descending
                    select count).ToArray();

        if (jokers == 5)
            type = [5];
        else
            type[0] += jokers;

        if (type[0] == 5)
            HandType = 6;
        else if (type[0] == 4)
            HandType = 5;
        else if (type[0] == 3 && type[1] == 2)
            HandType = 4;
        else if (type[0] == 3)
            HandType = 3;
        else if (type[0] == 2 && type[1] == 2)
            HandType = 2;
        else if (type[0] == 2)
            HandType = 1;
    }

    protected override Dictionary<char, int> SymbolsLookup { get; } = new() { { 'T', 10 }, { 'J', 1 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 } };
}

public static class Day7Extensions
{
    public static SortedList<T, int> ParseToCamelCardHands<T>(this IEnumerable<string> lines) where T : CamelCardHand
    {
        var sortedList = new SortedList<T, int>();
        foreach (var line in lines)
        {
            var values = line.Split(' ');
            sortedList.Add((T)Activator.CreateInstance(typeof(T), values[0].ToArray()), int.Parse(values[1]));
        }
        return sortedList;
    }
}