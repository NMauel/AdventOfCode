using System;

namespace AdventCode
{
    public static class Utils
    {
        public static void PrintMatrix<T>(T[,] matrix, char separator = '\t')
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    Console.Write($"{matrix[y, x]}{separator}");
                }
                Console.WriteLine();
            }
        }

        public static T[,] InitializeMatrix<T>(int x, int y, T initialValue)
        {
            var nums = new T[x, y];
            for (int i = 0; i < x * y; i++) nums[i % x, i / x] = initialValue;
            return nums;
        }
    }
}
