#pragma once

class Solution
{
public:
    bool exist(vector<vector<char>> &board, string word)
    {
        size_t rows = board.size();
        size_t columns = board.front().size();
        vector<pair<size_t, size_t>> path(word.size());

        for (size_t row = 0; row < rows; ++row)
        {
            for (size_t column = 0; column < columns; ++column)
            {
                if (board[row][column] == word.front())
                {
                    stack<pair<pair<size_t, size_t>, size_t>> s;

                    s.emplace(make_pair(row, column), 1);

                    while (!s.empty())
                    {
                        if (s.top().second == word.length())
                        {
                            return true;
                        }

                        auto item = s.top();

                        s.pop();

                        path[item.second - 1] = item.first;

                        for (auto next : { make_pair(item.first.first - 1, item.first.second),
                                           make_pair(item.first.first + 1, item.first.second),
                                           make_pair(item.first.first, item.first.second - 1),
                                           make_pair(item.first.first, item.first.second + 1) })
                        {
                            if (next.first < rows &&
                                next.second < columns &&
                                board[next.first][next.second] == word[item.second] &&
                                find(path.cbegin(), path.cbegin() + item.second, next) == path.cbegin() + item.second)
                            {
                                s.emplace(next, item.second + 1);
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
};
