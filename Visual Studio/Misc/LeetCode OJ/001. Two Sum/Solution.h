#pragma once

class Solution
{
public:
    vector<int> twoSum(const vector<int> &nums, int target)
    {
        unordered_map<int, size_t> numberToIndex;

        for (size_t i = 0; i < nums.size(); ++i)
        {
            auto it = numberToIndex.find(target - nums[i]);

            if (it == numberToIndex.cend())
            {
                numberToIndex.emplace(nums[i], i);
            }
            else
            {
                return{ static_cast<int>(it->second + 1), static_cast<int>(i + 1) };
            }
        }

        return{};
    }
};
