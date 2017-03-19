#pragma once

class Solution
{
public:
    void solve(vector<vector<char>> &board)
    {
        if (board.size() >= 3 && board.front().size() >= 3)
        {
            int row_count = board.size();
            int column_count = board.front().size();

            queue<pair<int, int>> q;

            for (int i = 0; i != row_count; ++i)
            {
                if (board[i][0] == 'O')
                {
                    board[i][0] = 'B';
                    q.emplace(i, 0);
                }
                if (board[i][column_count - 1] == 'O')
                {
                    board[i][column_count - 1] = 'B';
                    q.emplace(i, column_count - 1);
                }
            }

            for (int i = 1; i != column_count - 1; ++i)
            {
                if (board[0][i] == 'O')
                {
                    board[0][i] = 'B';
                    q.emplace(0, i);
                }
                if (board[row_count - 1][i] == 'O')
                {
                    board[row_count - 1][i] = 'B';
                    q.emplace(row_count - 1, i);
                }
            }

            while (!q.empty())
            {
                auto current = q.front();

                q.pop();

                for (auto p : { make_pair(current.first - 1, current.second),
                                make_pair(current.first, current.second - 1),
                                make_pair(current.first, current.second + 1),
                                make_pair(current.first + 1, current.second) })
                {
                    if (p.first >= 0 && p.first < row_count && p.second >= 0 && p.second < column_count &&
                        board[p.first][p.second] == 'O')
                    {
                        board[p.first][p.second] = 'B';
                        q.emplace(p);
                    }
                }
            }

            for (int i = 0; i != row_count; ++i)
            {
                for (int j = 0; j != column_count; ++j)
                {
                    if (board[i][j] == 'O')
                    {
                        board[i][j] = 'X';
                    }
                    else if (board[i][j] == 'B')
                    {
                        board[i][j] = 'O';
                    }
                }
            }
        }
    }
};
