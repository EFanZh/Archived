#pragma once

class Solution
{
public:
    vector<int> intersect(const vector<int> &nums1, const vector<int> &nums2)
    {
        auto m = unordered_map<int, int>();

        for (const auto &num : nums1)
        {
            ++m[num];
        }

        auto result = vector<int>();

        for (const auto &num : nums2)
        {
            const auto it = m.find(num);

            if (it != m.cend() && it->second > 0)
            {
                --it->second;

                result.emplace_back(num);
            }
        }

        return result;
    }
};
