#pragma once

static const int tenPows[] = { 0, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };

class Solution
{
public:
    bool isPalindrome(int x)
    {
        if (x < 0)
        {
            return false;
        }

        for (auto n = upper_bound(begin(tenPows), end(tenPows), x) - begin(tenPows) - 1; n > 0; n -= 2)
        {
            const auto high = x / tenPows[n];
            const auto low = x % 10;

            if (high != low)
            {
                return false;
            }

            x -= tenPows[n] * high;
            x /= 10;
        }

        return true;
    }
};
