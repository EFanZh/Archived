#pragma once

class Solution
{
public:
    bool containsNearbyDuplicate(const vector<int> &nums, int k)
    {
        auto indices = unordered_map<int, size_t>();

        for (auto i = size_t(0); i < nums.size(); ++i)
        {
            const auto it = indices.find(nums[i]);

            if (it == indices.cend())
            {
                indices.emplace(nums[i], i);
            }
            else
            {
                if (i - it->second <= static_cast<size_t>(k))
                {
                    return true;
                }
                else
                {
                    it->second = i;
                }
            }
        }

        return false;
    }
};
