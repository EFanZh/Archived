namespace Sorting
{
    internal abstract class QuickSortManager : SortManager
    {
        protected int Partition(int[] data, int p, int r)
        {
            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (this.IsTaskCanceled)
                {
                    return -1;
                }
                PostCompareCallback(j, r);
                if (data[j] <= data[r])
                {
                    i++;
                    PostSwapCallback(i, j);
                    data.Swap(i, j);
                }
            }
            this.PostSwapCallback(i + 1, r);
            data.Swap(i + 1, r);
            return i + 1;
        }
    }
}
