namespace Sorting
{
    internal class StandardBubbleSortManager : SortManager
    {
        protected override void DoSort(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = data.Length - 1; j > i; j--)
                {
                    this.PostCompareCallback(j, j - 1);
                    if (data[j - 1] > data[j])
                    {
                        this.PostSwapCallback(j - 1, j);
                        data.Swap(j - 1, j);
                    }
                    if (this.IsTaskCanceled)
                    {
                        return;
                    }
                }
            }
        }
    }
}
