#pragma once

class Solution
{
public:
    int firstMissingPositive(int A[], int n)
    {
        for (int i = 0; i < n; ++i)
        {
            while (A[i] != i + 1)
            {
                if (A[i] >= 1 && A[i] <= n && A[A[i] - 1] != A[i])
                {
                    swap(A[i], A[A[i] - 1]);
                }
                else
                {
                    A[i] = -1;
                    break;
                }
            }
        }

        return find(A, A + n, -1) - A + 1;
    }
};
