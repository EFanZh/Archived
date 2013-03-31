namespace Sorting
{
    internal class MergeSort3Manager : MergeSortManager
    {
        protected override void DoSort(int[] data)
        {
            int[] split = new int[data.Length + 1];

            split[0] = 0;
            int t = 1;
            for (int i = 1; i < data.Length; i++)
            {
                if (this.IsTaskCanceled)
                {
                    return;
                }
                this.PostCompareCallback(i - 1, i);
                if (data[i - 1] > data[i])
                {
                    split[t] = i;
                    t++;
                }
            }
            split[t] = data.Length;
            t++;

            while (t > 2)
            {
                for (int i = 0; i + 2 < t; i += 2)
                {
                    this.Merge(data, split[i], split[i + 1] - 1, split[i + 2] - 1);
                    if (this.IsTaskCanceled)
                    {
                        return;
                    }
                }
                t = t / 2 + 1;
                for (int i = 0; i < t - 1; i++)
                {
                    split[i] = split[2 * i];
                }
                split[t - 1] = data.Length;
            }
        }
    }
}
