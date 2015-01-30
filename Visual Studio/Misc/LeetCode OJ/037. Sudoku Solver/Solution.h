#pragma once

class Solution
{
public:
    void solveSudoku(vector<vector<char>> &board)
    {
        vector<pair<size_t, size_t>> blanks;
        array<array<bool, 9>, 9> rows = {};
        array<array<bool, 9>, 9> columns = {};
        array<array<bool, 9>, 9> blocks = {};

        for (size_t i = 0; i < 9; ++i)
        {
            for (size_t j = 0; j < 9; ++j)
            {
                if (board[i][j] == '.')
                {
                    blanks.emplace_back(i, j);
                }
                else
                {
                    rows[i][board[i][j] - '1'] = true;
                    columns[j][board[i][j] - '1'] = true;
                    blocks[i / 3 * 3 + j / 3][board[i][j] - '1'] = true;
                }
            }
        }

        function<bool(size_t)> tryFill = [&](size_t i)
        {
            if (i == blanks.size())
            {
                return true;
            }

            auto &position = blanks[i];
            auto &row = rows[position.first];
            auto &column = columns[position.second];
            auto &block = blocks[position.first / 3 * 3 + position.second / 3];

            for (int x = 0; x < 9; ++x)
            {
                if (!row[x] && !column[x] && !block[x])
                {
                    board[position.first][position.second] = static_cast<char>('1' + x);
                    row[x] = true;
                    column[x] = true;
                    block[x] = true;
                    if (tryFill(i + 1))
                    {
                        return true;
                    }
                    block[x] = false;
                    column[x] = false;
                    row[x] = false;
                    board[position.first][position.second] = '.';
                }
            }

            return false;
        };

        tryFill(0);
    }
};
