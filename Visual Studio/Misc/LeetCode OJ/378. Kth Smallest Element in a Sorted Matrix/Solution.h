#pragma once

class Solution
{
public:
    int kthSmallest(const vector<vector<int>> &matrix, int k)
    {
        const auto lastRow = matrix.size() - 1;
        const auto lastColumn = matrix.front().size() - 1;
        const auto totalSize = (lastRow + 1) * (lastColumn + 1);

        if (static_cast<size_t>(k) <= totalSize / 2)
        {
            auto comparer = [&](const pair<size_t, size_t> &lhs, const pair<size_t, size_t> &rhs) {
                return matrix[lhs.first][lhs.second] > matrix[rhs.first][rhs.second];
            };

            auto q =
                priority_queue<pair<size_t, size_t>, std::vector<pair<size_t, size_t>>, decltype(comparer)>(comparer);
            auto current = make_pair<size_t, size_t>(0, 0);

            for (;;)
            {
                if (k == 1)
                {
                    return matrix[current.first][current.second];
                }
                else
                {
                    --k;

                    if (current.first == 0)
                    {
                        if (current.second < lastColumn)
                        {
                            q.emplace(current.first, current.second + 1);
                        }

                        if (current.first < lastRow)
                        {
                            q.emplace(current.first + 1, current.second);
                        }
                    }
                    else
                    {
                        if (current.first < lastRow)
                        {
                            q.emplace(current.first + 1, current.second);
                        }
                    }

                    current = q.top();
                    q.pop();
                }
            }
        }
        else
        {
            k = static_cast<int>(totalSize - static_cast<size_t>(k));

            auto comparer = [&](const pair<size_t, size_t> &lhs, const pair<size_t, size_t> &rhs) {
                return matrix[lhs.first][lhs.second] < matrix[rhs.first][rhs.second];
            };

            auto q =
                priority_queue<pair<size_t, size_t>, std::vector<pair<size_t, size_t>>, decltype(comparer)>(comparer);
            auto current = make_pair(lastRow, lastColumn);

            for (;;)
            {
                if (k == 0)
                {
                    return matrix[current.first][current.second];
                }
                else
                {
                    --k;

                    if (current.first == lastRow)
                    {
                        if (current.second > 0)
                        {
                            q.emplace(current.first, current.second - 1);
                        }

                        if (current.first > 0)
                        {
                            q.emplace(current.first - 1, current.second);
                        }
                    }
                    else
                    {
                        if (current.first > 0)
                        {
                            q.emplace(current.first - 1, current.second);
                        }
                    }

                    current = q.top();
                    q.pop();
                }
            }
        }
    }
};
