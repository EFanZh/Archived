#pragma once

class Solution
{
public:
    int getSum(int a, int b)
    {
        while (b != 0)
        {
            tie(a, b) = make_tuple(a ^ b, (a & b) << 1);
        }

        return a;
    }
};
