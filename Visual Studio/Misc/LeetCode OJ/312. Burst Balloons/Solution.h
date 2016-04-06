#pragma once

class Solution
{
    static int maxCoinsHelper(const vector<int> &nums, vector<int>::size_type from, vector<int>::size_type to, vector<int> &cache)
    {
        if (from == to)
        {
            return 0;
        }

        int &cachedResult = cache[nums.size() * from + to - 1];

        if (cachedResult != -1)
        {
            return cachedResult;
        }

        auto result = 0;

        for (auto i = from; i < to; ++i)
        {
            const auto left = maxCoinsHelper(nums, from, i, cache);
            const auto middle = nums[from - 1] * nums[i] * nums[to];
            const auto right = maxCoinsHelper(nums, i + 1, to, cache);

            result = max(result, left + middle + right);
        }

        cachedResult = result;

        return result;
    }

public:
    int maxCoins(const vector<int> &nums)
    {
        vector<int> workingNums;

        workingNums.reserve(nums.size() + 2);
        workingNums.emplace_back(1);
        copy_if(nums.cbegin(), nums.cend(), back_inserter(workingNums), [](int x) { return x != 0; });
        workingNums.emplace_back(1);

        vector<int> cache(workingNums.size() * workingNums.size(), -1);

        return maxCoinsHelper(workingNums, 1, workingNums.size() - 1, cache);
    }
};
