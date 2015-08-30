#pragma once

class Solution
{
public:
    bool containsNearbyDuplicate(const vector<int> &nums, int k)
    {
        unordered_map<int, size_t> indices;

        for (size_t i = 0; i < nums.size(); ++i)
        {
            auto it = indices.find(nums[i]);

            if (it == indices.cend())
            {
                indices.emplace(nums[i], i);
            }
            else
            {
                if (i - it->second <= k)
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
