using System.Collections;
using System.Globalization;

namespace AdventCode
{
    public static class Extensions
    {
        public static IEnumerable<TResult> Pairwise<T, TResult>(this IEnumerable<T> enumerable, Func<T, T, TResult> resultSelector)
        {
            var previous = default(T);

            using var e = enumerable.GetEnumerator();
            if (e.MoveNext())
                previous = e.Current;

            while (e.MoveNext())
                yield return resultSelector(previous, previous = e.Current);
        }

        public static IEnumerable<TResult> Triowise<T, TResult>(this IEnumerable<T> enumerable, Func<T, T, T, TResult> resultSelector)
        {
            var first = default(T);
            var second = default(T);

            using var e = enumerable.GetEnumerator();
            if (e.MoveNext())
                first = e.Current;
            if (e.MoveNext())
                second = e.Current;

            while (e.MoveNext())
                yield return resultSelector(first, first = second, second = e.Current);
        }

        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            return !source.Any() ? source : source.Concat(source.SelectMany(i => selector(i) ?? Enumerable.Empty<T>()).SelectManyRecursive(selector));
        }

        public static int AsInt32(this BitArray bits)
        {
            var result = new int[1];
            bits.Reverse().CopyTo(result, 0);
            return result[0];
        }

        public static BitArray Reverse(this BitArray bits)
        {
            int len = bits.Count;
            BitArray a = new BitArray(bits);
            BitArray b = new BitArray(bits);

            for (int i = 0, j = len - 1; i < len; ++i, --j)
            {
                a[i] = a[i] ^ b[j];
                b[j] = a[i] ^ b[j];
                a[i] = a[i] ^ b[j];
            }

            return a;
        }

        /// <summary>
        /// Will return the multiplication of all incoming integers.
        /// Note: Supports only integers for now
        /// </summary>
        public static int Multiply(this IEnumerable<int> items)
        {
            var output = 1;
            foreach (var i in items)
                output *= i;
            return output;
        }

        public static (T, T) Map<T>(this string input, char split) => input.Map<T,T>(split.ToString());

        public static (T, T) Map<T>(this string input, string split) => input.Map<T, T>(split);

        public static (T, Tc) Map<T, Tc>(this string input, string split)
        {
            var splitResult = input.Split(split, StringSplitOptions.RemoveEmptyEntries);
            return ((T)Convert.ChangeType(splitResult[0], typeof(T)), (Tc)Convert.ChangeType(splitResult[1], typeof(Tc)));
        }

        public static T[,] ToMatrix<T>(this IEnumerable<IEnumerable<T>> field)
        {
            int x = 0, y = 0;
            var matrix = new T[field.Count(), field.First().Count()];

            foreach (var row in field)
            {
                foreach (var col in row)
                    matrix[y,x++] = col;
                x = 0;
                y++;
            }

            return matrix;
        }

        public static byte[] ConvertHexToBytes(this string hexString)
        {
            while (hexString.Length % 2 != 0)
                hexString = hexString += '0';

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
                data[index] = byte.Parse(hexString.Substring(index * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            return data;
        }

        public static T[] To1DArray<T>(this T[,] input)
        {
            int size = input.Length;
            T[] result = new T[size];

            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }

            return result;
        }
    }
}
