namespace AdventCode;

public static class Utils
{
    public static void PrintMatrix<T>(T[,] matrix, char separator = '\t')
    {
        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                Console.Write($"{matrix[y, x]}{separator}");
            }
            Console.WriteLine();
        }
    }

    public static T[,] InitializeMatrix<T>(int x, int y, T initialValue)
    {
        var nums = new T[x, y];
        for (var i = 0; i < x * y; i++)
        {
            nums[i % x, i / x] = initialValue;
        }
        return nums;
    }
}
