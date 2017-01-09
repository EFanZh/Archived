#pragma once

class Solution
{
public:
    int maxSumSubmatrix(const vector<vector<int>> &matrix, int k)
    {
        const auto rows = matrix.size();
        const auto columns = matrix.front().size();
        auto result = numeric_limits<int>::min();

        if (rows < columns)
        {
            for (auto i = size_t(0); i < rows; ++i)
            {
                auto columnSums = vector<int>(columns);

                for (auto j = i; j < rows; ++j)
                {
                    const auto &row = matrix[j];

                    for (auto column = size_t(0); column < columns; ++column)
                    {
                        columnSums[column] += row[column];
                    }

                    auto sums = set<int>({ 0 });
                    auto sum = 0;

                    for (auto s : columnSums)
                    {
                        sum += s;

                        const auto it = sums.lower_bound(sum - k);

                        if (it != sums.end())
                        {
                            result = max(result, sum - *it);
                        }

                        sums.emplace(sum);
                    }
                }
            }
        }
        else
        {
            for (auto i = size_t(0); i < columns; ++i)
            {
                auto rowSums = vector<int>(rows);

                for (auto j = i; j < columns; ++j)
                {
                    for (auto row = size_t(0); row < rows; ++row)
                    {
                        rowSums[row] += matrix[row][j];
                    }

                    auto sums = set<int>({ 0 });
                    auto sum = 0;

                    for (auto s : rowSums)
                    {
                        sum += s;

                        const auto it = sums.lower_bound(sum - k);

                        if (it != sums.end())
                        {
                            result = max(result, sum - *it);
                        }

                        sums.emplace(sum);
                    }
                }
            }
        }

        return result;
    }
};
