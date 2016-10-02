#pragma once

class Solution
{
public:
    bool increasingTriplet(const vector<int> &nums)
    {
        auto n1 = numeric_limits<int>::max();
        auto n2 = numeric_limits<int>::max();

        for (auto num : nums)
        {
            if (num <= n1)
            {
                n1 = num;
            }
            else if (num <= n2)
            {
                n2 = num;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
};
