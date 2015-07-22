#pragma once

class Solution
{
public:
    int rob(vector<int> &nums)
    {
        if (nums.size() == 0)
        {
            return 0;
        }
        else if (nums.size() == 1)
        {
            return nums.front();
        }

        int prev2 = nums.back();
        int prev1 = max(nums[nums.size() - 2], prev2);

        for (size_t i = nums.size() - 3; i < nums.size(); --i)
        {
            int current = max(nums[i] + prev2, prev1);

            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
};
