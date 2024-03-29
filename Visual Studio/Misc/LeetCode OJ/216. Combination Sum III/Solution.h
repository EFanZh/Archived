#pragma once

class Solution
{
    static size_t getMinSum(size_t first, size_t count)
    {
        return (first * 2 + count - 1) * count / 2;
    }

    static size_t getMaxSum(size_t count)
    {
        return (19 - count) * count / 2;
    }

    static void combinationSum3Helper(int first,
                                      size_t count,
                                      int target,
                                      vector<int> &base,
                                      vector<vector<int>> &result)
    {
        if (count == 1)
        {
            if (target >= first && target < 10)
            {
                base.emplace_back(target);
                result.emplace_back(base);
                base.pop_back();
            }
            return;
        }

        for (auto i = first; i < 10; ++i)
        {
            const auto newFirst = i + 1;
            const auto newCount = count - 1;
            const auto newTarget = target - i;

            if (getMinSum(i, count - 1) <= newTarget)
            {
                base.emplace_back(i);
                combinationSum3Helper(newFirst, newCount, newTarget, base, result);
                base.pop_back();
            }
            else
            {
                break;
            }
        }
    }

public:
    vector<vector<int>> combinationSum3(int k, int n)
    {
        if (getMinSum(1, k) > n || getMaxSum(k) < n)
        {
            return {};
        }

        vector<int> base;
        vector<vector<int>> result;

        combinationSum3Helper(1, k, n, base, result);

        return result;
    }
};
