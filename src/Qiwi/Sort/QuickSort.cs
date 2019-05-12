using System;

namespace Qiwi.Sort
{
    internal static class QuickSort<T>
    {
        internal static void SortAscending<TKey>(T[] array, int start, int end, Func<T, TKey> keySelector) where TKey : IComparable
        {
            Sort(array, start, end, (left, right) => Compare(keySelector(left), keySelector(right)));
        }
        
        internal static void SortDescending<TKey>(T[] array, int start, int end, Func<T, TKey> keySelector) where TKey : IComparable
        {
            Sort(array, start, end, (left, right) => Compare(keySelector(right), keySelector(left)));
        }

        internal static int Compare(IComparable left, IComparable right)
        {
            if (left == null && right == null) return 0;

            if (left == null && right != null) return -1;

            if (left != null && right == null) return 1;

            return left.CompareTo(right);
        }

        internal static void Sort(T[] array, int start, int end, Func<T, T, int> compare)
        {
            if (start >= end) return;

            var pivot = Partition(array, start, end, compare);

            Sort(array, start, pivot - 1, compare);
            Sort(array, pivot + 1, end, compare);
        }

        private static int Partition(T[] array, int start, int end, Func<T, T, int> compare)
        {
            var marker = start;
            var pivot = array[end];

            for (var i = start; i <= end; i++)
            {
                if (compare(array[i], pivot) <= 0)
                {
                    var temp = array[marker];
                    array[marker] = array[i];
                    array[i] = temp;
                    marker++;
                }
            }

            return marker - 1;
        }
    }
}
