#pragma once
class Solution
{
public:
    void gameOfLife(vector<vector<int>> &board)
    {
        static const pair<int8_t, int8_t> offsets[] = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

        if (board.empty() || board.front().empty())
        {
            return;
        }

        size_t rows = board.size();
        size_t columns = board.front().size();

        for (size_t row = 0; row < rows; ++row)
        {
            for (size_t column = 0; column < columns; ++column)
            {
                uint8_t neighbors = 0;

                for (const auto &offset : offsets)
                {
                    size_t neighborRow = row + offset.first;

                    if (neighborRow < rows)
                    {
                        size_t neighborColumn = column + offset.second;

                        if (neighborColumn < columns && (board[neighborRow][neighborColumn] & 1))
                        {
                            ++neighbors;
                        }
                    }
                }

                if (neighbors < 2 || (neighbors == 2 && board[row][column] == 0) || neighbors > 3)
                {
                    board[row][column] &= ~2;
                }
                else
                {
                    board[row][column] |= 2;
                }
            }
        }

        for (auto &row : board)
        {
            for (auto &column : row)
            {
                column >>= 1;
            }
        }
    }
};
