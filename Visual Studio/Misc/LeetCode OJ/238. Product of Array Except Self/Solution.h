#pragma once

class Solution
{
public:
    vector<int> productExceptSelf(const vector<int> &nums)
    {
        vector<int> result(nums.size(), 1);
        int k = nums.front();

        for (size_t i = 1; i < nums.size(); ++i)
        {
            result[i] = k;

            k *= nums[i];
        }

        k = nums.back();

        for (size_t i = nums.size() - 2; i < nums.size(); --i)
        {
            result[i] *= k;

            k *= nums[i];
        }

        return result;
    }
};
