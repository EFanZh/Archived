#pragma once

class Solution
{
public:
    int nthUglyNumber(int n)
    {
        vector<int> cache(n);
        size_t i5 = 0, i3 = 0, i2 = 0, last = 0;

        cache.front() = 1;

        for (int i = 1; i < n; ++i)
        {
            int oldNumber = cache[last];
            int k5 = oldNumber / 5;
            int k3 = oldNumber / 3;
            int k2 = oldNumber / 2;

            if (cache[i5] == k5)
            {
                ++i5;
            }

            if (cache[i3] == k3)
            {
                ++i3;
            }

            if (cache[i2] == k2)
            {
                ++i2;
            }

            cache[++last] = min(min(cache[i5] * 5, cache[i3] * 3), cache[i2] * 2);
        }

        return cache[last];
    }
};
