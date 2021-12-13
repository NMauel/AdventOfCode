using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public static (T,T) Map<T>(this string input, char split)
        {
            var splitResult = input.Split(split, StringSplitOptions.RemoveEmptyEntries);
            return ((T)Convert.ChangeType(splitResult[0], typeof(T)), (T)Convert.ChangeType(splitResult[1], typeof(T)));
        }
    }
}
