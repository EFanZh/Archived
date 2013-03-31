namespace Sorting
{
    internal class SelectionSortManager : SortManager
    {
        protected override void DoSort(int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < data.Length; j++)
                {
                    if (this.IsTaskCanceled)
                    {
                        return;
                    }
                    this.PostCompareCallback(j, min);
                    if (data[j] < data[min])
                    {
                        min = j;
                    }
                }
                if (i != min)
                {
                    this.PostSwapCallback(i, min);
                    data.Swap(i, min);
                }
            }
        }
    }
}
