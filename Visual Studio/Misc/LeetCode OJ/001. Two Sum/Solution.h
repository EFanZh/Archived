#pragma once

class Solution
{
public:
    vector<int> twoSum(vector<int> &numbers, int target)
    {
        unordered_map<int, size_t> numberToIndex;

        for (size_t i = 0; i < numbers.size(); ++i)
        {
            auto it = numberToIndex.find(target - numbers[i]);

            if (it == numberToIndex.cend())
            {
                numberToIndex.emplace(numbers[i], i);
            }
            else
            {
                return { static_cast<int>(it->second + 1), static_cast<int>(i + 1) };
            }
        }

        return {};
    }
};
