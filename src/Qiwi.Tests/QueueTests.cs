using System;
using Xunit;
using MsGenerics = System.Collections.Generic;
using System.Linq;

using Qiwi.Collections;

namespace Qiwi.Tests
{
    public class QueueTests
    {
        [Fact]
        public void EnqueueDequeue_BehaviorEqualsMsQueue()
        {
            var msQueue = new MsGenerics.Queue<int>(new[] { 7, 5, 6, 7, 1, 2 });
            var queue = new Queue<int>(msQueue);
                       
            while (msQueue.Count != 0)
            {
                Assert.Equal(msQueue.Count, queue.Count);
                Assert.Equal(msQueue.Dequeue(), queue.Dequeue());
            }
        }

        [Fact]
        public void SortByAscending_ReturnsOrderedQueue()
        {
            var integers = new[] { 2, 5, 6, 7, 1, 2 };
                        
            var queue = new Queue<int>(integers);
            
            queue.OrderBy(i => i);
            Array.Sort(integers);

            Assert.Equal(integers.Length, queue.Count);

            for (var i = 0; i < integers.Length; i++)
                Assert.Equal(integers[i], queue.Dequeue());
        }

        [Fact]
        public void SortByDescending_ReturnsOrderedQueue()
        {
            var integers = new[] { 2, 5, 6, 7, 1, 2 };

            var queue = new Queue<int>(integers);

            queue.OrderByDescending(i => i);
            Array.Sort(integers);
            Array.Reverse(integers);

            Assert.Equal(integers.Length, queue.Count);

            for (var i = 0; i < integers.Length; i++)
                Assert.Equal(integers[i], queue.Dequeue());
        }

        [Fact]
        public void Dequeue_ThenSortByAscending_ReturnsOrderedQueue()
        {
            var integers = new MsGenerics.List<int> { 2, 5, 6, 7, 1, 2 };

            var queue = new Queue<int>(integers);
            queue.Dequeue();
            integers.RemoveAt(0);

            queue.OrderBy(i => i);
            integers.Sort();

            Assert.Equal(integers.Count, queue.Count);

            for (var i = 0; i < integers.Count; i++)
                Assert.Equal(integers[i], queue.Dequeue());
        }

        [Fact]
        public void Dequeue_ThenSortByDescending_ReturnsOrderedQueue()
        {
            var integers = new MsGenerics.List<int> { 2, 5, 6, 7, 1, 2 };

            var queue = new Queue<int>(integers);
            queue.Dequeue();
            integers.RemoveAt(0);

            queue.OrderByDescending(i => i);
            integers = integers.OrderByDescending(i => i).ToList();

            Assert.Equal(integers.Count, queue.Count);

            for (var i = 0; i < integers.Count; i++)
                Assert.Equal(integers[i], queue.Dequeue());
        }

        [Fact]
        public void SortByAscending_QueueContainsNull_ReturnsOrderedQueue()
        {
            var strings = new [] {"a", "v", null, "z", "b", "e" };

            var queue = new Queue<string>(strings);

            queue.OrderBy(s => s);
            Array.Sort(strings);

            Assert.Equal(strings.Length, queue.Count);

            for (var i = 0; i < strings.Length; i++)
                Assert.Equal(strings[i], queue.Dequeue());
        }

        [Fact]
        public void SortByDescending_QueueContainsNull_ReturnsOrderedQueue()
        {
            var strings = new[] { "a", "v", null, "z", "b", "e" };

            var queue = new Queue<string>(strings);

            queue.OrderByDescending(s => s);
            strings = strings.OrderByDescending(s => s).ToArray();

            Assert.Equal(strings.Length, queue.Count);

            for (var i = 0; i < strings.Length; i++)
                Assert.Equal(strings[i], queue.Dequeue());
        }
    }
}
