#pragma once

class Solution
{
    static bool lessThanOrEqualUnsigned(int lhs, int rhs)
    {
        return static_cast<unsigned int>(lhs) <= static_cast<unsigned int>(rhs);
    }

public:
    bool containsNearbyAlmostDuplicate(const vector<int> &nums, int k, int t)
    {
        if (t < 0)
        {
            return false;
        }

        multiset<int> current;

        for (size_t i = 0; i < nums.size(); ++i)
        {
            size_t old = i - (k + 1);

            if (old < nums.size())
            {
                current.erase(current.find(nums[old]));
            }

            auto it = current.lower_bound(nums[i]);

            if (it != current.cend() && lessThanOrEqualUnsigned(*it - nums[i], t))
            {
                return true;
            }

            if (it != current.cbegin() && lessThanOrEqualUnsigned(nums[i] - *--it, t))
            {
                return true;
            }

            current.emplace(nums[i]);
        }

        return false;
    }
};
