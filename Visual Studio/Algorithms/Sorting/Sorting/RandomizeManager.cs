namespace Sorting
{
    internal class RandomizeManager : SortManager
    {
        protected override void DoSort(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                int k = Utilities.Random.Next(i, data.Length);
                this.PostSwapCallback(i, k);
                data.Swap(i, k);
                if (IsTaskCanceled)
                {
                    return;
                }
            }
        }
    }
}
