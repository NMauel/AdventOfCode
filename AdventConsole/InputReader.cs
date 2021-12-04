using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    public static class InputReader
    {
        private const string inputFolder = "2021/Inputs";

        public static IEnumerable<string> ReadLines(string fileName) => File.ReadAllLines($"{inputFolder}/{fileName}");
    }
}
