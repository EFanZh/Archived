#pragma once

class Solution
{
    static vector<int> pascalTriangleRow(size_t count)
    {
        auto result = vector<int>(count);
        auto middle = (count + 1) / 2;

        result.front() = 1;

        for (auto i = size_t(1); i < middle; ++i)
        {
            result[i] = result[i - 1] * (count - i) / i;
        }

        copy(result.cbegin(), result.cbegin() + count / 2, result.rbegin());

        return result;
    }

public:
    int uniquePaths(int m, int n)
    {
        // Make sure m is the smaller one.
        if (n < m)
        {
            swap(m, n);
        }

        if (m == 1)
        {
            return 1;
        }

        auto cache = pascalTriangleRow(m);

        for (auto i = size_t(0); i < static_cast<size_t>(n - m); ++i)
        {
            for (auto j = size_t(0); j < static_cast<size_t>(m - 1); ++j)
            {
                cache[j] += cache[j + 1];
            }
        }

        for (auto i = size_t(0); i < static_cast<size_t>(m - 1); i++)
        {
            for (auto j = size_t(0); j < static_cast<size_t>(m - 1 - i); j++)
            {
                cache[j] += cache[j + 1];
            }
        }

        return cache.front();
    }
};
