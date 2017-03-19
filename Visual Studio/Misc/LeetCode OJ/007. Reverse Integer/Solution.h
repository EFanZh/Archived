#pragma once

class Solution
{
public:
    int reverse(int x)
    {
        long long result = 0;

        while (x != 0)
        {
            result = result * 10 + x % 10;
            x /= 10;
        }

        return numeric_limits<int>::min() <= result && result <= numeric_limits<int>::max() ? static_cast<int>(result) :
                                                                                              0;
    }
};
