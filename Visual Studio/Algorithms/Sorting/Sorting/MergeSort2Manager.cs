namespace Sorting
{
    internal class MergeSort2Manager : MergeSortManager
    {
        protected override void DoSort(int[] data)
        {
            while (true)
            {
                int p = 0, q = 0, r = -1;
                while (r < data.Length - 1)
                {
                    p = r + 1;
                    q = p;
                    while (q + 1 < data.Length && data[q] <= data[q + 1])
                    {
                        this.PostCompareCallback(q, q + 1);
                        q++;
                    }
                    r = q + 1;
                    while (r + 1 < data.Length && data[r] <= data[r + 1])
                    {
                        this.PostCompareCallback(r, r + 1);
                        r++;
                    }
                    if (r < data.Length)
                    {
                        this.Merge(data, p, q, r);
                        if (this.IsTaskCanceled)
                        {
                            return;
                        }
                        if (r == data.Length - 1 && p == 0)
                        {
                            return;
                        }
                    }
                    else if (p == 0)
                    {
                        return;
                    }
                }
            }
        }
    }
}
