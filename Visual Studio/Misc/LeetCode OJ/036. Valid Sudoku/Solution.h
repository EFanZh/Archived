#pragma once

class Solution
{
public:
    bool isValidSudoku(vector<vector<char>> &board)
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
                    if (rows[i][board[i][j] - '1'] || columns[j][board[i][j] - '1'] ||
                        blocks[i / 3 * 3 + j / 3][board[i][j] - '1'])
                    {
                        return false;
                    }
                    else
                    {
                        rows[i][board[i][j] - '1'] = true;
                        columns[j][board[i][j] - '1'] = true;
                        blocks[i / 3 * 3 + j / 3][board[i][j] - '1'] = true;
                    }
                }
            }
        }

        return true;
    }
};
