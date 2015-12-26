using System;
using System.Collections;
using System.Collections.Generic;

namespace BouncingBall
{
    internal class FixedSizeQueue<T> : IEnumerable<T>, ICollection
    {
        private readonly T[] buffer;
        private int head = 0;
        private int length = 0;

        private class Enumerator : IEnumerator<T>
        {
            private readonly FixedSizeQueue<T> queue;
            private int index = -1;

            public Enumerator(FixedSizeQueue<T> queue)
            {
                this.queue = queue;
            }

            public T Current => queue.buffer[queue.GetBufferIndex(index)];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                ++index;

                return index < queue.length;
            }

            public void Reset()
            {
                index = -1;
            }
        }

        public FixedSizeQueue(int maxCount)
        {
            this.buffer = new T[maxCount];
        }

        public int Count => length;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public int MaxCount => buffer.Length;

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            for (var i = 0; i < length; i++)
            {
                array.SetValue(buffer[GetBufferIndex(i)], index + i);
            }
        }

        public void Enqueue(T value)
        {
            buffer[GetBufferIndex(length)] = value;

            if (length == MaxCount)
            {
                ++head;
                head %= MaxCount;
            }
            else
            {
                ++length;
            }
        }

        private int GetBufferIndex(int index)
        {
            return (head + index) % MaxCount;
        }
    }
}
