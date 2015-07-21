#pragma once

// Boyer-Moore Vote Algorithm
class Solution
{
public:
    int majorityElement(vector<int> &nums)
    {
        int result = nums.front();
        size_t votes = 1;

        for (size_t i = 1; i < nums.size(); ++i)
        {
            if (nums[i] == result)
            {
                ++votes;
            }
            else
            {
                if (votes == 0)
                {
                    result = nums[i];
                    votes = 1;
                }
                else
                {
                    --votes;
                }
            }
        }

        return result;
    }
};
