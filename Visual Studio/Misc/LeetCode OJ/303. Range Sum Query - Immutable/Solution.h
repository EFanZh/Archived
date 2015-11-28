#pragma once

class NumArray
{
    vector<int> cache;

public:
    NumArray(const vector<int> &nums) : cache(nums.size() + 1)
    {
        for (size_t i = 0; i < nums.size(); ++i)
        {
            cache[i + 1] = cache[i] + nums[i];
        }
    }

    int sumRange(int i, int j)
    {
        return cache[j + 1] - cache[i];
    }
};

// Your NumArray object will be instantiated and called as such:
// NumArray numArray(nums);
// numArray.sumRange(0, 1);
// numArray.sumRange(1, 2);
