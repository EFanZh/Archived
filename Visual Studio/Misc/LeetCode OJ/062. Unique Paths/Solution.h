#pragma once

class Solution
{
    static vector<int> pascalTriangleRow(int count)
    {
        vector<int> result(count);
        size_t middle = (count + 1) / 2;

        result.front() = 1;

        for (size_t i = 1; i < middle; ++i)
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

        vector<int> cache = pascalTriangleRow(m);

        for (size_t i = 0; i < n - m; ++i)
        {
            for (size_t j = 0; j < m - 1; ++j)
            {
                cache[j] += cache[j + 1];
            }
        }

        for (size_t i = 0; i < m - 1; i++)
        {
            for (size_t j = 0; j < m - 1 - i; j++)
            {
                cache[j] += cache[j + 1];
            }
        }

        return cache.front();
    }
};
