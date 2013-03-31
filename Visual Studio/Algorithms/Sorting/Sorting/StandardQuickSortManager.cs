namespace Sorting
{
    internal class StandardQuickSortManager : QuickSortManager
    {
        private void QuickSort(int[] data, int p, int r)
        {
            if (p < r)
            {
                int q = this.Partition(data, p, r);
                QuickSort(data, p, q - 1);
                if (this.IsTaskCanceled)
                {
                    return;
                }
                QuickSort(data, q + 1, r);
                if (this.IsTaskCanceled)
                {
                    return;
                }
            }
        }

        protected override void DoSort(int[] data)
        {
            QuickSort(data, 0, data.Length - 1);
        }
    }
}
