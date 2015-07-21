#pragma once

class Solution
{
public:
    int findMin(vector<int> &nums)
    {
        if (nums.front() < nums.back())
        {
            return nums.front();
        }
        else if (nums.front() == nums.back())
        {
            auto it = find_if(nums.cbegin(), nums.cend(), [&](int x) { return x != nums.front(); });

            if (it == nums.cend())
            {
                return nums.front();
            }

            if (*it > nums.front())
            {
                return *lower_bound(it, nums.cend(), nums.front(), greater<int>());
            }
            else
            {
                return *it;
            }
        }
        else
        {
            return *lower_bound(nums.cbegin(), nums.cend(), nums.front(), greater_equal<int>());
        }
    }
};
