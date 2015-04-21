#pragma once

class Solution
{
    template <class T>
    static vector<vector<int>> subsetsHelper(T first, T last)
    {
        vector<vector<int>> result = { {} };

        for (auto current = first; current != last; ++current)
        {
            for (auto &k : subsetsHelper(current + 1, last))
            {
                k.emplace(k.cbegin(), *current);

                result.emplace_back(move(k));
            }
        }

        return result;
    }

public:
    vector<vector<int>> subsets(vector<int> &S)
    {
        sort(S.begin(), S.end());

        return subsetsHelper(S.cbegin(), S.cend());
    }
};
