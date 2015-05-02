using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    internal class XSortManager : SortManager
    {
        private void Sort(int[] data, int index, int length)
        {
            if (length == 1)
            {
                return;
            }

            int mid = length / 2;

            Sort(data, index, length / 2);
            Sort(data, index + mid, length - length / 2);

            for (int i = 0; i < mid; i++)
            {
                int a = index + i;
                int b = index + length - 1 - i;

                if (this.IsTaskCanceled)
                {
                    return;
                }
                this.PostCompareCallback(a, b);
                if (data[a] > data[b])
                {
                    this.PostSwapCallback(a, b);
                    data.Swap(a, b);
                }
            }
        }

        protected override void DoSort(int[] data)
        {
            Sort(data, 0, data.Length);
        }
    }
}
