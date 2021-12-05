using System.Collections.Generic;
using System.IO;

namespace AdventCode
{
    public static class InputReader
    {
        public static string ReadString(string fileName) => File.ReadAllText(@$"2021\Inputs\{fileName}");
        public static IEnumerable<string> ReadLines(string fileName) => File.ReadAllLines(@$"2021\Inputs\{fileName}");
    }
}
