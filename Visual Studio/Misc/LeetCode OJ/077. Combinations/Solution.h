#pragma once

class Solution
{
    static void combineHelper(int first, int last, size_t i, vector<int> &current, vector<vector<int>> &result)
    {
        for (int n = first; n <= last; ++n)
        {
            current[i] = n;

            if (i == current.size() - 1)
            {
                result.emplace_back(current);
            }
            else
            {
                combineHelper(n + 1, last, i + 1, current, result);
            }
        }
    }

public:
    vector<vector<int>> combine(int n, int k)
    {
        vector<vector<int>> result;
        vector<int> current(k);

        combineHelper(1, n, 0, current, result);

        return result;
    }
};
