#pragma once

class Solution
{
public:
    int jump(int A[], int n)
    {
        int a = 0, b = 0;
        int step = 0;
        int newB = 0;

        while (newB < n - 1)
        {
            for (int i = a; i <= b; ++i)
            {
                newB = max(newB, i + A[i]);
            }
            a = b + 1;
            b = newB;
            ++step;
        }

        return step;
    }
};
