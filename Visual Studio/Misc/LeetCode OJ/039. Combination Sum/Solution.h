#pragma once

class Solution
{
    static vector<vector<int>> combinationSumHelper(const vector<int> &candidates, size_t first, int target)
    {
        if (target == 0)
        {
            return { {} };
        }

        vector<vector<int>> result;

        for (size_t i = first; i < candidates.size() && candidates[i] <= target; ++i)
        {
            for (auto &k : combinationSumHelper(candidates, i, target - candidates[i]))
            {
                k.emplace(k.cbegin(), candidates[i]);
                result.emplace_back(move(k));
            }
        }

        return result;
    }

public:
    vector<vector<int>> combinationSum(vector<int> &candidates, int target)
    {
        sort(candidates.begin(), candidates.end());

        return combinationSumHelper(candidates, 0, target);
    }
};
