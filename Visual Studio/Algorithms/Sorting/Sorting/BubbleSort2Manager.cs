namespace Sorting
{
    internal class BubbleSort2Manager : SortManager
    {
        protected override void DoSort(int[] data)
        {
            int last = 0;
            while (last < data.Length - 1)
            {
                int i = last;
                for (int j = data.Length - 1; j > i; j--)
                {
                    if (this.IsTaskCanceled)
                    {
                        return;
                    }
                    this.PostCompareCallback(j, j - 1);
                    if (data[j - 1] > data[j])
                    {
                        this.PostSwapCallback(j, j - 1);
                        data.Swap(j, j - 1);
                        last = j;
                    }
                }
                if (last == i)
                {
                    break;
                }
            }
        }
    }
}
