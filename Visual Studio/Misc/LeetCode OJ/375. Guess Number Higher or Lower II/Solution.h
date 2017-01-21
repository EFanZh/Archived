#pragma once

class Solution
{
public:
    int getMoneyAmount(int n)
    {
        // Half of memory space is wasted.
        auto cache = vector<int>(n * n);
        const auto cacheOffset = &cache.front() - (n + 1);
        auto lastOffset = &cache.front() - 1;

        for (auto last = 2; last <= n; ++last)
        {
            lastOffset += n;

            for (auto first = last - 1; first > 0; --first)
            {
                auto result = min(first + lastOffset[first + 1], last + lastOffset[first - n]);

                for (auto pick = first + 1; pick <= last - 1; ++pick)
                {
                    result = min(result, pick + max(cacheOffset[n * (pick - 1) + first], lastOffset[pick + 1]));
                }

                lastOffset[first] = result;
            }
        }

        return cacheOffset[n * n + 1];
    }
};
