#pragma once

class Solution
{
    static string buildRange(int first, int last)
    {
        string result = to_string(first);

        if (last > first)
        {
            result += "->";
            result += to_string(last);
        }

        return result;
    }

public:
    vector<string> summaryRanges(const vector<int> &nums)
    {
        vector<string> result;

        for (size_t i = 0; i < nums.size();)
        {
            int base = nums[i];

            for (++i; i < nums.size() && nums[i] == nums[i - 1] + 1; ++i)
            {
                continue;
            }

            result.emplace_back(buildRange(base, nums[i - 1]));
        }

        return result;
    }
};
