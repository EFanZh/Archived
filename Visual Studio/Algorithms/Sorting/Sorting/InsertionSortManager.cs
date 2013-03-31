namespace Sorting
{
    internal class InsertionSortManager : SortManager
    {
        protected override void DoSort(int[] data)
        {
            for (int j = 1; j < data.Length; j++)
            {
                int key = data[j];
                int i = j - 1;
                while (i >= 0 && data[i] > key)
                {
                    this.PostSetValueIndirectCallback(i + 1, i);
                    data[i + 1] = data[i];
                    i--;
                }
                this.PostSetValueCallback(i + 1, key);
                data[i + 1] = key;
                if (this.IsTaskCanceled)
                {
                    return;
                }
            }
        }
    }
}
