#pragma once

class Solution
{
public:
    int findPeakElement(vector<int> &nums)
    {
        if (nums.size() == 1)
        {
            return 0;
        }

        if (nums[0] > nums[1])
        {
            return 0;
        }

        if (nums[nums.size() - 1] > nums[nums.size() - 2])
        {
            return static_cast<int>(nums.size() - 1);
        }

        size_t left = 1;
        size_t count = nums.size() - 2;

        while (count > 0)
        {
            size_t middle = left + count / 2;

            if (nums[middle] > nums[middle - 1])
            {
                if (nums[middle] > nums[middle + 1])
                {
                    return middle;
                }
                else
                {
                    count -= middle + 1 - left;
                    left = middle + 1;
                }
            }
            else
            {
                count = middle - left;
            }
        }

        return -1;
    }
};
