#pragma once

class Solution
{
    static bool canPut(vector<string> &board, size_t row, size_t column)
    {
        for (size_t offset = 1; offset <= row && offset <= column; ++offset)
        {
            if (board[row - offset][column - offset] == 'Q')
            {
                return false;
            }
        }

        for (size_t offset = 1; offset <= row && offset < board.size() - column; ++offset)
        {
            if (board[row - offset][column + offset] == 'Q')
            {
                return false;
            }
        }

        return true;
    }

    static void solve(vector<string> &board, vector<char> columns, size_t row, vector<vector<string>> &result)
    {
        if (row == board.size())
        {
            result.emplace_back(board);
        }
        else
        {
            for (size_t column = 0; column < board.size(); ++column)
            {
                if (!columns[column] && canPut(board, row, column))
                {
                    board[row][column] = 'Q';
                    columns[column] = true;
                    solve(board, columns, row + 1, result);
                    columns[column] = false;
                    board[row][column] = '.';
                }
            }
        }
    }

public:
    vector<vector<string>> solveNQueens(int n)
    {
        vector<vector<string>> result;
        vector<string> board(n, string(n, '.'));
        vector<char> usedColumns(n, false);

        solve(board, usedColumns, 0, result);

        return result;
    }
};
