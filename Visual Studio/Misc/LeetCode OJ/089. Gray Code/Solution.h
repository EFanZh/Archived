#pragma once

class Solution
{
public:
    vector<int> grayCode(int n)
    {
        vector<int> result;

        result.reserve(size_t(1) << n);
        result.emplace_back(0);

        for (int i = 0; i < n; ++i)
        {
            int base = 1 << i;

            for (int j = base - 1; j >= 0; --j)
            {
                result.emplace_back(base + result[j]);
            }
        }

        return result;
    }
};
