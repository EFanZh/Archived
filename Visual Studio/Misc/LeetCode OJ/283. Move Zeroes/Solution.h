#pragma once

class Solution
{
public:
    void moveZeroes(vector<int> &nums)
    {
        fill(remove(nums.begin(), nums.end(), 0), nums.end(), 0);
    }
};
