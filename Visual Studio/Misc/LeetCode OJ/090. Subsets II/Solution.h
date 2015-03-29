#pragma once

class Solution
{
    static vector<vector<int>> subsetsWithDupHelper(map<int, size_t> &numbers)
    {
        if (numbers.empty())
        {
            return { {} };
        }

        vector<vector<int>> result;
        auto it = numbers.cbegin();
        auto current = *it;

        numbers.erase(it);
        for (size_t i = 0; i <= current.second; ++i)
        {
            for (auto &k : subsetsWithDupHelper(numbers))
            {
                result.emplace_back(i, current.first);
                move(k.begin(), k.end(), back_inserter(result.back()));
            }
        }
        numbers.emplace(current);

        return result;
    }

public:
    vector<vector<int>> subsetsWithDup(vector<int> &S)
    {
        map<int, size_t> numbers;

        for (auto i : S)
        {
            ++numbers[i];
        }

        return subsetsWithDupHelper(numbers);
    }
};
