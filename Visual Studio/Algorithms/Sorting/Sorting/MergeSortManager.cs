namespace Sorting
{
    internal abstract class MergeSortManager : SortManager
    {
        protected void Merge(int[] data, int p, int q, int r)
        {
            if (this.IsTaskCanceled)
            {
                return;
            }
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] left = new int[n1 + 1], right = new int[n2 + 1];
            for (int i = 0; i < n1; i++)
            {
                left[i] = data[p + i];
            }
            for (int j = 0; j < n2; j++)
            {
                right[j] = data[q + 1 + j];
            }
            left[n1] = int.MaxValue;
            right[n2] = int.MaxValue;
            int i2 = 0;
            int j2 = 0;
            for (int k = p; k <= r; k++)
            {
                if (left[i2] <= right[j2])
                {
                    this.PostSetValueCallback(k, left[i2]);
                    data[k] = left[i2];
                    i2++;
                }
                else
                {
                    this.PostSetValueCallback(k, right[j2]);
                    data[k] = right[j2];
                    j2++;
                }
            }
        }
    }
}
