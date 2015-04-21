#pragma once

class Solution2
{
public:
    vector<vector<int>> subsets(vector<int> &S)
    {
        vector<vector<int>> result = { {} };

        sort(S.begin(), S.end());

        result.reserve(1 << S.size());

        for (int current : S)
        {
            auto newSets = result;

            for (auto &k : newSets)
            {
                k.emplace_back(current);
            }

            move(newSets.begin(), newSets.end(), back_inserter(result));
        }

        return result;
    }
};
