#pragma once

class Solution
{
public:
    int minSubArrayLen(int s, vector<int> &nums)
    {
        size_t minLength = 0;
        size_t first = 0;
        size_t last = 0;
        int sum = 0;

        for (size_t i = 0; i < nums.size(); ++i)
        {
            last = i + 1;
            sum += nums[i];
            while (sum - nums[first] >= s)
            {
                sum -= nums[first];
                ++first;
            }

            if (minLength == 0)
            {
                if (sum >= s)
                {
                    minLength = last - first;
                }
            }
            else
            {
                minLength = min(minLength, last - first);
            }
        }

        return minLength;
    }
};
