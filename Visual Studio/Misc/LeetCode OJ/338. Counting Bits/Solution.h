#pragma once

class Solution
{
public:
    vector<int> countBits(int num)
    {
        auto result = vector<int>(num + 1);

        for (auto i = vector<int>::size_type(1); i < result.size(); ++i)
        {
            result[i] = result[i & (i - 1)] + 1;
        }

        return result;
    }
};
