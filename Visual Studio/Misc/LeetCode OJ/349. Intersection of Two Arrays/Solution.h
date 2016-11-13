#pragma once

class Solution
{
    static vector<int> intersectionHelper(const vector<int> &nums1, const vector<int> &nums2)
    {
        auto s = unordered_set<int>(nums1.cbegin(), nums1.cend());
        auto result = vector<int>();

        for (auto i : nums2)
        {
            if (s.erase(i) > 0)
            {
                result.emplace_back(i);
            }
        }

        return result;
    }

public:
    vector<int> intersection(const vector<int> &nums1, const vector<int> &nums2)
    {
        if (nums1.size() < nums2.size())
        {
            return intersectionHelper(nums1, nums2);
        }
        else
        {
            return intersectionHelper(nums2, nums1);
        }
    }
};
