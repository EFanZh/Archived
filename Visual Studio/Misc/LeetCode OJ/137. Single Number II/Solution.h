#pragma once

class Solution
{
public:
    int singleNumber(int A[], int n)
    {
        int ones = 0;
        int twos = 0;

        for (int i = 0; i != n; ++i)
        {
            ones = (ones ^ A[i]) & ~twos;
            twos = (twos ^ A[i]) & ~ones;
        }

        return ones;
    }
};
