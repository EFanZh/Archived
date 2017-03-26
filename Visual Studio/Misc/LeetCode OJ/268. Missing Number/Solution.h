#pragma once

class Solution
{
public:
    int missingNumber(const vector<int> &nums)
    {
        return static_cast<int>(nums.size() * (nums.size() + 1) / 2 - accumulate(nums.cbegin(), nums.cend(), 0));
    }
};
