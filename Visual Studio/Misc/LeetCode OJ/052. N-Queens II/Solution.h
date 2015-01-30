#pragma once

class Solution
{
    static bool canPut(size_t size, vector<char> &board, size_t row, size_t column)
    {
        for (size_t offset = 1; offset <= row && offset <= column; ++offset)
        {
            if (board[size * (row - offset) + column - offset])
            {
                return false;
            }
        }

        for (size_t offset = 1; offset <= row && offset < size - column; ++offset)
        {
            if (board[size * (row - offset) + column + offset])
            {
                return false;
            }
        }

        return true;
    }

    static int totalNQueensHelper(size_t size, vector<char> &board, vector<char> &usedColumns, size_t row)
    {
        if (row == size)
        {
            return 1;
        }

        int result = 0;

        for (size_t column = 0; column < size; ++column)
        {
            if (!usedColumns[column] && canPut(size, board, row, column))
            {
                board[size * row + column] = true;
                usedColumns[column] = true;
                result += totalNQueensHelper(size, board, usedColumns, row + 1);
                usedColumns[column] = false;
                board[size * row + column] = false;
            }
        }

        return result;
    }

public:
    int totalNQueens(int n)
    {
        vector<char> board(n * n, false);
        vector<char> usedColumns(n, false);

        return totalNQueensHelper(n, board, usedColumns, 0);
    }
};
