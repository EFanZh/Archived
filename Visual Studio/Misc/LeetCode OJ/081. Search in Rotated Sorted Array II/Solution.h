#pragma once

class Solution
{
public:
    bool search(vector<int> &nums, int target)
    {
        if (nums.empty())
        {
            return false;
        }

        if (nums.front() == target)
        {
            return true;
        }

        if (nums.size() == 1)
        {
            return false;
        }

        // Sorted.
        if (nums.front() < nums.back())
        {
            return binary_search(nums.cbegin(), nums.cend(), target);
        }

        vector<int>::const_iterator first;

        if (nums.front() == nums.back())
        {
            first = find_if(nums.cbegin() + 1, nums.cend(), [&](int x) { return x != nums.front(); });

            if (first == nums.cend())
            {
                return false;
            }
            else if (*first < nums.front())
            {
                return binary_search(first, nums.cend(), target);
            }
        }
        else
        {
            first = nums.cbegin();
        }

        auto middle = lower_bound(first, nums.cend(), *first, greater_equal<int>());

        if (target < nums.front())
        {
            return binary_search(middle, nums.cend(), target);
        }
        else
        {
            return binary_search(first, middle, target);
        }
    }
};
