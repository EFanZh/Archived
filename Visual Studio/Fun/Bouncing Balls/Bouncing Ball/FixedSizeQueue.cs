using System;
using System.Collections;
using System.Collections.Generic;

namespace BouncingBall
{
    internal class FixedSizeQueue<T> : IEnumerable<T>, ICollection, IEnumerable
    {
        private readonly Queue<T> queue = new Queue<T>();
        private int maxCount;

        public FixedSizeQueue(int maxCount)
        {
            this.maxCount = maxCount;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)queue).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)queue).CopyTo(array, index);
        }

        public int Count => queue.Count;

        public object SyncRoot => ((ICollection)queue).SyncRoot;

        public bool IsSynchronized => ((ICollection)queue).IsSynchronized;

        public int MaxCount
        {
            get
            {
                return maxCount;
            }
            set
            {
                maxCount = value;

                while (queue.Count > maxCount)
                {
                    queue.Dequeue();
                }
            }
        }

        public void Enqueue(T item)
        {
            if (queue.Count == MaxCount)
            {
                queue.Dequeue();
            }

            queue.Enqueue(item);
        }
    }
}
