#pragma once
class Solution
{
public:
    void gameOfLife(vector<vector<int>> &board)
    {
        static const pair<size_t, size_t> offsets[] = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 },
                                                        { 0, 1 },   { 1, -1 }, { 1, 0 },  { 1, 1 } };

        if (board.empty() || board.front().empty())
        {
            return;
        }

        const auto rows = board.size();
        const auto columns = board.front().size();

        for (auto row = size_t(0); row < rows; ++row)
        {
            for (auto column = size_t(0); column < columns; ++column)
            {
                auto neighbors = uint8_t(0);

                for (const auto &offset : offsets)
                {
                    const auto neighborRow = row + offset.first;

                    if (neighborRow < rows)
                    {
                        const auto neighborColumn = column + offset.second;

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
