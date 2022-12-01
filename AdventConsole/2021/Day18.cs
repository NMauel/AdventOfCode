namespace AdventCode.Aoc2021
{
    public class Day18 : IPuzzleDay
    {
        private readonly IEnumerable<string> input = InputReader.ReadLines();
        
        public object CalculateAnswerPuzzle1()
        {
            var numbers = ParseInput();

            Node totalNumber = numbers.First();
            foreach (var number in numbers.Skip(1))
            {
                totalNumber += number;
                Reduce(totalNumber);
            }

            return totalNumber.Magnitude;
        }

        public object CalculateAnswerPuzzle2()
        {
            var maxMagnitude = 0;

            for (var i = 0; i < input.Count(); i++)
                for (var j = 0; j < input.Count(); j++)
                {
                    if (i == j)
                        continue;
                    var numbers = ParseInput().ToArray();
                    var sumNumber = numbers[i] + numbers[j];
                    Reduce(sumNumber);
                    maxMagnitude = Math.Max(maxMagnitude, sumNumber.Magnitude);
                }

            return maxMagnitude;
        }

        private void Reduce(Node node)
        {
            while (true)
            {
                if (Explode(node))
                    continue;
                if (Split(node))
                    continue;
                break;
            }
        }

        private bool Explode(Node node)
        {
            var level = 0;
            Node explodingNode = null;
            List<Node> traversalList = new();

            Traverse(node);

            if(explodingNode != null)
            {
                var leftIndex = traversalList.IndexOf(explodingNode.Left);
                var rightIndex = leftIndex + 1;

                if (leftIndex - 1 >= 0)
                    traversalList[leftIndex - 1].Value += explodingNode.Left.Value;
                if (rightIndex + 1 < traversalList.Count)
                    traversalList[rightIndex + 1].Value += explodingNode.Right.Value;

                explodingNode.Value = 0;
            }

            return explodingNode != null;

            void Traverse(Node node)
            {
                level++;
                if (node.IsNumber)
                    traversalList.Add(node);
                else
                {
                    Traverse(node.Left);
                    Traverse(node.Right);

                    if (level > 4 && explodingNode == null)
                        explodingNode = node;
                }
                level--;
            }
        }

        private bool Split(Node node)
        {
            if (node.IsNumber)
            {
                if (node.Value > 9)
                {
                    node.SetNodes(new Node(node.Value / 2), new Node(node.Value / 2 + node.Value % 2));
                    return true;
                }
                return false;
            }
            return Split(node.Left) || Split(node.Right);
        }

        private IEnumerable<Node> ParseInput()
        {
            foreach(var line in input)
                yield return ParseNode(new Queue<char>(line.Substring(1)));

            static Node ParseNode(Queue<char> charQueue)
            {
                Node left = null;
                Node right = null;

                var c = charQueue.Dequeue();
                if (c == '[')
                    left = ParseNode(charQueue);
                else if (char.IsNumber(c))
                    left = new Node(c - 48);

                charQueue.Dequeue();

                c = charQueue.Dequeue();
                if (c == '[')
                    right = ParseNode(charQueue);
                else if (char.IsNumber(c))
                    right = new Node(c - 48);

                charQueue.Dequeue();

                return new Node(left, right);
            }
        }

        private class Node
        {
            private int _value = -1;

            public bool IsNumber => _value >= 0;

            public int Value { 
                get => _value;
                set { 
                    _value = value;
                    Left = null;
                    Right = null;
                }
            }
            public Node Left { get; private set; }
            public Node Right { get; private set; }

            public Node(int value) => _value = value;

            public Node(Node left, Node right)
            {
                Left = left;
                Right = right;
            }

            public void SetNodes(Node left, Node right)
            {
                Left = left;
                Right = right;
                _value = -1;
            }

            public int Magnitude => IsNumber
                ? Value
                : 3 * Left.Magnitude + 2 * Right.Magnitude;

            public static Node operator +(Node nodeOrg, Node nodeAdd) => new(nodeOrg, nodeAdd);
        }
    }
}