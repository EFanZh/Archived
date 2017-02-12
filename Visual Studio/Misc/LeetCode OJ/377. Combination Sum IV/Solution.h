#pragma once

class Solution
{
public:
    int combinationSum4(const vector<int> &nums, int target)
    {
        auto cache = vector<int>(target + 1);

        cache.front() = 1;

        for (auto i = 1; i <= target; ++i)
        {
            for (auto n : nums)
            {
                if (i >= n)
                {
                    cache[i] += cache[i - n];
                }
            }
        }

        return cache.back();
    }
};
