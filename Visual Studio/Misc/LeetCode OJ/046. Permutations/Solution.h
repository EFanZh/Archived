#pragma once

class Solution
{
    static size_t factorial(size_t n)
    {
        size_t result = 1;

        for (size_t i = 2; i <= n; ++i)
        {
            result *= i;
        }

        return result;
    }

public:
    vector<vector<int>> permute(vector<int> &nums)
    {
        if (nums.empty())
        {
            return { {} };
        }

        vector<vector<int>> result;

        result.reserve(factorial(nums.size()));

        int saved = move(nums.front());

        nums.erase(nums.cbegin());

        for (auto &item : permute(nums))
        {
            item.emplace(item.cbegin(), saved);
            result.emplace_back(move(item));
        }

        for (size_t i = 0; i < nums.size(); ++i)
        {
            swap(saved, nums[i]);

            for (auto &item : permute(nums))
            {
                item.emplace(item.cbegin(), saved);
                result.emplace_back(move(item));
            }
        }

        nums.emplace_back(saved);

        return result;
    }
};
