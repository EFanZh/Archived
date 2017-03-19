#pragma once

class NumMatrix
{
    size_t cacheColumns;
    vector<int> cache;

    int &getCache(size_t row, size_t column)
    {
        return cache[cacheColumns * row + column];
    }

public:
    NumMatrix(const vector<vector<int>> &matrix)
        : cacheColumns(matrix.empty() ? 1 : matrix.front().size() + 1), cache((matrix.size() + 1) * cacheColumns)
    {
        vector<int> rowCache(cacheColumns);

        for (size_t row = 1; row <= matrix.size(); ++row)
        {
            const auto &currentRow = matrix[row - 1];

            for (size_t column = 1; column <= currentRow.size(); ++column)
            {
                rowCache[column] = rowCache[column - 1] + currentRow[column - 1];

                getCache(row, column) = getCache(row - 1, column) + rowCache[column];
            }
        }
    }

    int sumRegion(int row1, int col1, int row2, int col2)
    {
        return getCache(row2 + 1, col2 + 1) - getCache(row2 + 1, col1) - getCache(row1, col2 + 1) +
               getCache(row1, col1);
    }
};

// Your NumMatrix object will be instantiated and called as such:
// NumMatrix numMatrix(matrix);
// numMatrix.sumRegion(0, 1, 2, 3);
// numMatrix.sumRegion(1, 2, 3, 4);
