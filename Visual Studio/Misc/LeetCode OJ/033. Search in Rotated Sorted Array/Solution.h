#pragma once

class Solution
{
    template <class T, class TValue>
    static int searchHelper(T first, T last, TValue value)
    {
        auto it = lower_bound(first, last, value);

        if (it == last || *it != value)
        {
            return -1;
        }
        else
        {
            return it - first;
        }
    }

public:
    int search(vector<int> &nums, int target)
    {
        if (nums.empty())
        {
            return -1;
        }

        const auto it = lower_bound(nums.cbegin(), nums.cend(), nums.back(), greater<int>());

        if (target < nums.back())
        {
            const auto result = searchHelper(it, nums.cend() - 1, target);

            if (result == -1)
            {
                return -1;
            }
            else
            {
                return (it - nums.cbegin()) + result;
            }
        }
        else if (nums.back() < target)
        {
            return searchHelper(nums.cbegin(), it, target);
        }
        else
        {
            return static_cast<int>(nums.size() - 1);
        }
    }
};
