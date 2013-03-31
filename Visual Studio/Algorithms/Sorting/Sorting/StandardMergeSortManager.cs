namespace Sorting
{
    internal class StandardMergeSortManager : MergeSortManager
    {
        private void MergeSort(int[] data, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MergeSort(data, p, q);
                MergeSort(data, q + 1, r);
                this.Merge(data, p, q, r);
            }
        }

        protected override void DoSort(int[] data)
        {
            MergeSort(data, 0, data.Length - 1);
        }
    }
}
