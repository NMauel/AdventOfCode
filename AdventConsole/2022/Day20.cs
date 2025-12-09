namespace AdventCode.Aoc2022;

public class Day20 : IPuzzleDay
{
    public object CalculateAnswerPuzzle1()
    {
        var magic = new MagicList(InputReader.ReadLines().Select(i => (Guid.NewGuid(), long.Parse(i))));
        var itemsToMix = new Queue<(Guid Id, long Value)>(magic);

        return MixAndCalc(magic, itemsToMix);
    }

    public object CalculateAnswerPuzzle2()
    {
        var magic = new MagicList(InputReader.ReadLines().Select(i => (Guid.NewGuid(), long.Parse(i) * 811589153)));
        var itemsToMix = new Queue<(Guid Id, long Value)>(Enumerable.Repeat(magic, 10).SelectMany(m => m));

        return MixAndCalc(magic, itemsToMix);
    }

    private long MixAndCalc(MagicList magic, Queue<(Guid Id, long Value)> itemsToMix)
    {
        magic.Mix(itemsToMix);

        var zeroIndex = magic.IndexOf(magic.Single(x => x.Value == 0));

        return magic[zeroIndex + 1000] + magic[zeroIndex + 2000] + magic[zeroIndex + 3000];
    }

    public class MagicList : List<(Guid Id, long Value)>
    {
        public MagicList(IEnumerable<(Guid, long)> values)
        {
            AddRange(values);
        }

        public new long this[int index] => base[index % Count].Value;

        public void Mix(Queue<(Guid Id, long Value)> itemsToMix)
        {
            while (itemsToMix.Any())
            {
                var item = itemsToMix.Dequeue();
                var newIndex = (int)((IndexOf(item) + item.Value) % (Count - 1));

                if (newIndex < 1)
                    newIndex = Count - 1 + newIndex;

                Remove(item);
                Insert(newIndex, item);
            }
        }

        public override string ToString() => string.Join(',', this.Select(x => x.Value));
    }
}
