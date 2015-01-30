#pragma once

class Solution
{
public:
    int maxSubArray(int A[], int n)
    {
        int minSum = min(0, A[0]);
        int result = A[0];
        int currentSum = A[0];

        for (int i = 1; i != n; ++i)
        {
            currentSum += A[i];

            if (currentSum - minSum > result)
            {
                result = currentSum - minSum;
            }

            if (currentSum < minSum)
            {
                minSum = currentSum;
            }
        }

        return result;
    }
};
