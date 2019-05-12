using System;
using System.Collections.Generic;

using Qiwi.Helpers;
using Qiwi.Sort;

namespace Qiwi.Collections
{
    public class Queue<T>
    {
        private static readonly T[] emptyArray = new T[0];

        private T[] array;
        private int head;
        private int tail;
        private int size;

        private const int MinimumGrow = 4;
        private const int DefaultCapacity = 4;
        private const int GrowFactor = 200;

        public Queue() {
            array = emptyArray;
        }

        public Queue(IEnumerable<T> collection)
        {
            if (collection == null)
                ThrowHelper.ThrowArgumentNullException(nameof(collection));

            array = new T[DefaultCapacity];
            size = 0;

            using (IEnumerator<T> e = collection.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Enqueue(e.Current);
                }
            }
        }

        public int Count => size;
               
        public void Enqueue(T item)
        {
            if (size == array.Length)
            {
                var capacity = array.Length * GrowFactor / 100;

                if (capacity < array.Length + MinimumGrow)
                    capacity = array.Length + MinimumGrow;

                SetCapacity(capacity);
            }

            array[tail] = item;
            tail = (tail + 1) % array.Length;
            size++;
        }

        public T Dequeue()
        {
            if (size == 0)
                ThrowHelper.ThrowInvalidOperationException("The queue is empty.");

            var removed = array[head];
            array[head] = default(T);
            head = (head + 1) % array.Length;

            size--;

            return removed;
        }

        public Queue<T> OrderBy<TKey>(Func<T, TKey> keySelector) where TKey : IComparable
        {
            if (keySelector == null)
                ThrowHelper.ThrowArgumentNullException(nameof(keySelector));

            if (size <= 1) return this;

            if (head > tail)
                SetCapacity(array.Length);
            
            QuickSort<T>.SortAscending(array, head, GetEndIndex(), keySelector);

            return this;
        }
        
        public Queue<T> OrderByDescending<TKey>(Func<T, TKey> keySelector) where TKey : IComparable
        {
            if (keySelector == null)
                ThrowHelper.ThrowArgumentNullException(nameof(keySelector));

            if (size <= 1) return this;
            
            if (head > tail)
                SetCapacity(array.Length);

            QuickSort<T>.SortDescending(array, head, GetEndIndex(), keySelector);

            return this;
        }

        private int GetEndIndex()
        {
            return tail == 0 ? size - 1 : tail - 1;
        }

        private void SetCapacity(int capacity)
        {
            var newArray = new T[capacity];

            if (size >= 0)
            {
                if (head < tail)
                {
                    Array.Copy(array, head, newArray, 0, size);
                }
                else
                {
                    Array.Copy(array, head, newArray, 0, array.Length - head);
                    Array.Copy(array, 0, newArray, array.Length - head, tail);
                }
            }

            array = newArray;
            head = 0;
            tail = size == capacity ? 0 : size;
        }
    }
}
