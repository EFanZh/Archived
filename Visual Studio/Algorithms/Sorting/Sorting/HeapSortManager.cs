namespace Sorting
{
    internal class HeapSortManager : SortManager
    {
        private int Parent(int i)
        {
            return (i - 1) / 2;
        }

        private int Left(int i)
        {
            return 2 * i + 1;
        }

        private int Right(int i)
        {
            return 2 * i + 2;
        }

        private void MaxHeapify(int[] data, int i, int heap_size)
        {
            if (this.IsTaskCanceled)
            {
                return;
            }
            int l = Left(i);
            int r = Right(i);
            int largest;
            this.PostCompareCallback(l, i);
            if (l < heap_size && data[l] > data[i])
            {
                largest = l;
            }
            else
            {
                largest = i;
            }
            this.PostCompareCallback(r, largest);
            if (r < heap_size && data[r] > data[largest])
            {
                largest = r;
            }
            if (largest != i)
            {
                this.PostSwapCallback(i, largest);
                data.Swap(i, largest);
                MaxHeapify(data, largest, heap_size);
            }
        }

        private int BuildMaxHeap(int[] data)
        {
            for (int i = Parent(data.Length - 1); i >= 0; i--)
            {
                MaxHeapify(data, i, data.Length);
                if (this.IsTaskCanceled)
                {
                    return -1;
                }
            }
            return data.Length;
        }

        protected override void DoSort(int[] data)
        {
            int heap_size = BuildMaxHeap(data);
            for (int i = data.Length - 1; i > 0; i--)
            {
                this.PostSwapCallback(0, i);
                data.Swap(0, i);
                heap_size--;
                MaxHeapify(data, 0, heap_size);
                if (this.IsTaskCanceled)
                {
                    return;
                }
            }
        }
    }
}
