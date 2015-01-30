#pragma once

class Solution
{
    static vector<vector<int>> combinationSum2Helper(const vector<int> &num, size_t last, int target)
    {
        if (target == 0)
        {
            return { {} };
        }
        else if (target < num.front())
        {
            return {};
        }

        vector<vector<int>> result;

        for (size_t i = 0; i < last; ++i)
        {
            vector<vector<int>> newResult = combinationSum2Helper(num, i, target - num[i]);

            for (size_t j = 0; j < newResult.size(); ++j)
            {
                newResult[j].emplace_back(num[i]);
            }

            move(newResult.begin(), newResult.end(), back_inserter(result));
        }

        return result;
    }

public:
    vector<vector<int>> combinationSum2(vector<int> &num, int target)
    {
        sort(num.begin(), num.end());

        auto result = combinationSum2Helper(num, num.size(), target);

        sort(result.begin(), result.end(), [](const vector<int> &lhs, const vector<int> &rhs)
        {
            for (size_t i = 0; i < lhs.size() && i < rhs.size(); ++i)
            {
                if (lhs[i] < rhs[i])
                {
                    return true;
                }
                else if (rhs[i] < lhs[i])
                {
                    return false;
                }
            }

            return lhs.size() < rhs.size();
        });
        result.erase(unique(result.begin(), result.end()), result.end());

        return result;
    }
};
