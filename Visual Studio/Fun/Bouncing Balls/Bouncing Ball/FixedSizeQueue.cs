using System;
using System.Collections;
using System.Collections.Generic;

namespace BouncingBall
{
    internal class FixedSizeQueue<T> : IEnumerable<T>, ICollection, IEnumerable
    {
        private readonly Queue<T> queue = new Queue<T>();
        private int maxCount;

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

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        public object SyncRoot
        {
            get
            {
                return ((ICollection)queue).SyncRoot;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return ((ICollection)queue).IsSynchronized;
            }
        }

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
            while (queue.Count >= MaxCount)
            {
                queue.Dequeue();
            }

            queue.Enqueue(item);
        }
    }
}
