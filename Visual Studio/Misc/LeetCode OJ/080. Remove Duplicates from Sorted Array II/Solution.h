#pragma once

class Solution
{
public:
    int removeDuplicates(vector<int> &nums)
    {
        for (auto i = size_t(0); i < nums.size();)
        {
            const auto current = nums[i];

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

        return static_cast<int>(nums.size());
    }
};
