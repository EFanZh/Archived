#pragma once

class Solution
{
    template <class T>
    static int robHelper(T first, T last)
    {
        if (first == last)
        {
            return 0;
        }
        else if (first + 1 == last)
        {
            return *first;
        }

        int prev2 = *(last - 1);
        int prev1 = max(*(last - 2), prev2);

        for (auto it = last - 3; it >= first; --it)
        {
            int current = max(*it + prev2, prev1);

            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }

public:
    int rob(const vector<int> &nums)
    {
        if (nums.empty())
        {
            return 0;
        }
        else if (nums.size() < 4)
        {
            return *max_element(nums.cbegin(), nums.cend());
        }

        int robFirst = nums.front() + robHelper(nums.cbegin() + 2, nums.cend() - 1);
        int notRobFirst = robHelper(nums.cbegin() + 1, nums.cend());

        return max(robFirst, notRobFirst);
    }
};
