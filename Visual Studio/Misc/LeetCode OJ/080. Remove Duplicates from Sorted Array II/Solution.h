#pragma once

class Solution
{
public:
    int removeDuplicates(vector<int> &nums)
    {
        for (size_t i = 0; i < nums.size();)
        {
            int current = nums[i];

            ++i;
            if (i == nums.size() || nums[i] != current)
            {
                continue;
            }

            ++i;
            while (i < nums.size() && nums[i] == current)
            {
                nums.erase(nums.cbegin() + i);
            }
        }

        return nums.size();
    }
};
