#pragma once

class Solution
{
public:
    double findMedianSortedArrays(int A[], int m, int B[], int n)
    {
        // Make sure that A is the shorter one.
        if (m > n)
        {
            swap(m, n);
            swap(A, B);
        }

        int t = (m + n) / 2 - 1;

        // Find the first one in A that is greater or equals to the median.
        int left = 0, right = m;

        while (left < right)
        {
            int middle = (left + right) / 2;

            if (t - middle >= 0 && A[middle] < B[t - middle])
            {
                left = middle + 1;
            }
            else
            {
                right = middle;
            }
        }

        if ((m + n) % 2 == 0)
        {
            int a = max(left == 0 ? numeric_limits<int>::min() : A[left - 1], t - left < 0 ? numeric_limits<int>::min() : B[t - left]);
            int b = min(left == m ? numeric_limits<int>::max() : A[left], t - left + 1 == n ? numeric_limits<int>::max() : B[t - left + 1]);

            return (a + b) / 2.0;
        }
        else
        {
            return left == m ? B[t - left + 1] : min(A[left], B[t - left + 1]);
        }
    }
};
