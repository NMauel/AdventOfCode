namespace AdventCode.Aoc2022;

public class Day7 : IPuzzleDay
{
    private readonly Item _fileSystem = ParseCommandsToFileSystem(InputReader.ReadLines());

    public object CalculateAnswerPuzzle1() => _fileSystem.Items.SelectManyRecursive(x => x.Items).Where(x => x.IsDir && x.Size <= 100000).Sum(x => x.Size);

    public object CalculateAnswerPuzzle2() => _fileSystem.Items.SelectManyRecursive(x => x.Items).Where(x => x.IsDir && x.Size >= _fileSystem.Size - (70000000 - 30000000)).Min(x => x.Size);

    private static Item ParseCommandsToFileSystem(IEnumerable<string> commands)
    {
        Item root = new("/", 0);
        var currentDir = root;

        foreach (var command in commands)
        {
            if (command.StartsWith("$ cd"))
            {
                var dirName = command.Replace("$ cd ", string.Empty);
                if (dirName == "/")
                    currentDir = root;
                else if (dirName == "..")
                    currentDir = currentDir.Parent;
                else
                    currentDir = currentDir.Items.First(x => x.Name == dirName);
            }
            else if (command.StartsWith("$ ls"))
            {
                //Do nothing...
            }
            else if (command.StartsWith("dir"))
            {
                var dir = command.Replace("dir ", string.Empty);
                if (!currentDir.Items.Any(x => x.Name == dir))
                    currentDir.Items.Add(new(dir, 0, currentDir));
            }
            else
            {
                var info = command.Split(' ');
                currentDir.Items.Add(new(info[1], int.Parse(info[0]), currentDir));
            }
        }

        return root;
    }

    private sealed class Item
    {
        private readonly int _size;

        public Item(string name, int size, Item parent = null)
        {
            Name = name;
            _size = size;
            Parent = parent ?? this;//No parent is top level root... :-)
        }

        public Item Parent { get; }
        public string Name { get; }
        public int Size => IsDir ? Items.Sum(i => i.Size) : _size;
        public bool IsDir => _size == 0;
        public List<Item> Items { get; } = new();
    }
}
