#pragma once

class Solution
{
public:
    int lengthOfLIS(const vector<int> &nums)
    {
        if (nums.size() < 2)
        {
            return static_cast<int>(nums.size());
        }

        vector<int> cache(nums.size());
        auto first = cache.begin();
        auto last = first + 1;

        cache.front() = nums.front();

        for (size_t i = 1; i < nums.size(); ++i)
        {
            auto it = lower_bound(first, last, nums[i]);

            if (it == last)
            {
                ++last;
            }

            *it = nums[i];
        }

        return static_cast<int>(last - first);
    }
};
