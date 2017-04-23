#pragma once

class Solution
{
    int helper(unordered_map<int, int> &cache, int n)
    {
        if (n < 4)
        {
            return n - 1;
        }
        else
        {
            const auto it = cache.find(n);

            if (it != cache.end())
            {
                return it->second;
            }
            else
            {
                const auto half = n / 2;

                if (n % 2 == 0)
                {
                    return cache.emplace(n, helper(cache, half) + 1).first->second;
                }
                else
                {
                    return cache.emplace(n, min(helper(cache, half), helper(cache, half + 1)) + 2).first->second;
                }
            }
        }
    }

public:
    int integerReplacement(int n)
    {
        auto cache = unordered_map<int, int>();

        return helper(cache, n);
    }
};
