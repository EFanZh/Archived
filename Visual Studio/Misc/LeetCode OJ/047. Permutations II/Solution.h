#pragma once

class Solution
{
    static vector<vector<int>> permuteUniqueHelper(vector<int> &num)
    {
        if (num.size() == 1)
        {
            return { num };
        }

        vector<vector<int>> result;

        for (size_t i = 0; i < num.size();)
        {
            int k = num[i];

            num.erase(num.begin() + i);

            auto newResult = permuteUniqueHelper(num);

            num.emplace(num.begin() + i, k);

            for (auto &v : newResult)
            {
                v.emplace(v.begin(), k);
            }

            move(newResult.begin(), newResult.end(), back_inserter(result));

            ++i;
            while (i < num.size() && num[i] == k)
            {
                ++i;
            }
        }

        return result;
    }

public:
    vector<vector<int>> permuteUnique(vector<int> &num)
    {
        sort(num.begin(), num.end());

        return permuteUniqueHelper(num);
    }
};
