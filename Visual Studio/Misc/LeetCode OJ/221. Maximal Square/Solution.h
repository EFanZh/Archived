#pragma once

class Solution
{
public:
    int maximalSquare(const vector<vector<char>> &matrix)
    {
        if (matrix.empty() || matrix.front().empty())
        {
            return 0;
        }

        const size_t rows = matrix.size();
        const size_t columns = matrix.front().size();
        vector<int> squareSizes(columns);
        int result = 0;

        transform(matrix.front().cbegin(), matrix.front().cend(), squareSizes.begin(), [&](char c) {
            if (c == '1')
            {
                result = 1;

                return 1;
            }
            else
            {
                return 0;
            }
        });

        for (size_t row = 1; row < rows; ++row)
        {
            squareSizes.front() = matrix[row].front() == '1' ? 1 : 0;
            result = max(result, squareSizes.front());

            for (size_t column = 1; column < columns; ++column)
            {
                if (matrix[row][column] == '1')
                {
                    int leftSize = squareSizes[column - 1];
                    int topSize = squareSizes[column];

                    if (leftSize == topSize)
                    {
                        if (matrix[row - leftSize][column - leftSize] == '1')
                        {
                            squareSizes[column] = leftSize + 1;
                        }
                        else
                        {
                            squareSizes[column] = leftSize;
                        }
                    }
                    else
                    {
                        squareSizes[column] = min(leftSize, topSize) + 1;
                    }

                    result = max(result, squareSizes[column]);
                }
                else
                {
                    squareSizes[column] = 0;
                }
            }
        }

        return result * result;
    }
};
