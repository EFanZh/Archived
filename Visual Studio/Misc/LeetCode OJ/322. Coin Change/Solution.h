#pragma once

class Solution
{
public:
    int coinChange(const vector<int> &coins, int amount)
    {
        auto cache = vector<int>(amount + 1);

        for (auto i = 1; i <= amount; ++i)
        {
            auto result = amount;

            for (auto n : coins)
            {
                if (n <= i)
                {
                    result = min(result, cache[i - n]);
                }
            }

            cache[i] = result + 1;
        }

        if (cache.back() <= amount)
        {
            return cache.back();
        }
        else
        {
            return -1;
        }
    }
};
