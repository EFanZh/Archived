#pragma once

class Solution
{
public:
    int findMin(vector<int> &nums)
    {
        // If sorted.
        if (nums.front() <= nums.back())
        {
            return nums.front();
        }

        return *lower_bound(nums.cbegin(), nums.cend(), nums.front(), greater_equal<int>());
    }
};
