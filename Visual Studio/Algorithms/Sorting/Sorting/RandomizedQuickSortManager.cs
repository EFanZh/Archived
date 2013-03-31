namespace Sorting
{
    internal class RandomizedQuickSortManager : QuickSortManager
    {
        private int RandomizedPartition(int[] data, int p, int r)
        {
            int i = Utilities.Random.Next(p, r + 1);
            this.PostSwapCallback(r, i);
            data.Swap(r, i);
            return this.Partition(data, p, r);
        }

        private void RandomizedQuickSort(int[] data, int p, int r)
        {
            if (p < r)
            {
                int q = RandomizedPartition(data, p, r);
                RandomizedQuickSort(data, p, q - 1);
                if (this.IsTaskCanceled)
                {
                    return;
                }
                RandomizedQuickSort(data, q + 1, r);
                if (this.IsTaskCanceled)
                {
                    return;
                }
            }
        }

        protected override void DoSort(int[] data)
        {
            RandomizedQuickSort(data, 0, data.Length - 1);
        }
    }
}
