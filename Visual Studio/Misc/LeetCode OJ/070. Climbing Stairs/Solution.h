#pragma once

class Solution
{
public:
    int climbStairs(int n)
    {
        int a = 1;
        int b = 1;

        for (int i = 0; i != n; ++i)
        {
            b += a;
            a = b - a;
        }

        return a;
    }
};
