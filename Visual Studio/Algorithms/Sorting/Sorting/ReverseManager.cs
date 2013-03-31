namespace Sorting
{
    internal class ReverseManager : SortManager
    {
        protected override void DoSort(int[] data)
        {
            int i = 0, j = data.Length - 1;
            while (i < j)
            {
                if (this.IsTaskCanceled)
                {
                    return;
                }
                PostSwapCallback(i, j);
                data.Swap(i, j);
                i++;
                j--;
            }
        }
    }
}
