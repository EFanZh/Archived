#pragma once

class Solution
{
    template <class T>
    static int minPathSumHelper(size_t rows, size_t columns, T getCell)
    {
        if (rows == 1)
        {
            int result = 0;

            for (size_t i = 0; i < columns; ++i)
            {
                result += getCell(0, i);
            }

            return result;
        }

        vector<int> cache(rows);

        cache.back() = getCell(rows - 1, columns - 1);

        for (size_t i = rows - 2; i < rows - 1; --i)
        {
            cache[i] = getCell(i, columns - 1) + cache[i + 1];

            for (size_t j = i + 1; j < rows - 1; ++j)
            {
                cache[j] = getCell(j, columns - 1 + i - j) + min(cache[j], cache[j + 1]);
            }

            cache[rows - 1] += getCell(rows - 1, columns - rows + i);
        }

        for (size_t i = columns - 2; i > rows - 2; --i)
        {
            for (size_t j = 0; j < rows - 1; ++j)
            {
                cache[j] = getCell(j, i - j) + min(cache[j], cache[j + 1]);
            }

            cache[rows - 1] += getCell(rows - 1, i - (rows - 1));
        }

        for (size_t i = rows - 1; i < rows; --i)
        {
            for (size_t j = 0; j < i; ++j)
            {
                cache[j] = getCell(j, i - 1 - j) + min(cache[j], cache[j + 1]);
            }
        }

        return cache.front();
    }

public:
    int minPathSum(vector<vector<int>> &grid)
    {
        size_t rows = grid.size();

        if (rows == 0)
        {
            return 0;
        }

        size_t columns = grid.front().size();

        if (columns == 0)
        {
            return 0;
        }

        if (rows < columns)
        {
            return minPathSumHelper(rows, columns, [&](size_t row, size_t column) { return grid[row][column]; });
        }
        else
        {
            return minPathSumHelper(columns, rows, [&](size_t row, size_t column) { return grid[column][row]; });
        }
    }
};
