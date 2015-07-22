#pragma once

class Solution
{
public:
    int numIslands(vector<vector<char>> &grid)
    {
        if (grid.empty() || grid.front().empty())
        {
            return 0;
        }

        size_t rows = grid.size();
        size_t columns = grid.front().size();
        vector<char> visited(columns * rows);
        int result = 0;

        auto refVisited = [&](size_t row, size_t column) -> char &
        {
            return visited[columns * row + column];
        };

        auto bfs = [&](size_t row, size_t column)
        {
            queue<pair<size_t, size_t>> q;

            refVisited(row, column) = true;
            q.emplace(row, column);

            while (!q.empty())
            {
                auto current = q.front();

                q.pop();
                for (auto offset : { make_pair(-1, 0), make_pair(0, -1), make_pair(1, 0), make_pair(0, 1) })
                {
                    pair<size_t, size_t> next(current.first + offset.first, current.second + offset.second);

                    if (next.first < rows &&
                        next.second < columns &&
                        !refVisited(next.first, next.second) &&
                        grid[next.first][next.second] == '1')
                    {
                        refVisited(next.first, next.second) = true;
                        q.emplace(next);
                    }
                }
            }
        };

        for (size_t row = 0; row < rows; ++row)
        {
            for (size_t column = 0; column < columns; ++column)
            {
                if (!refVisited(row, column) && grid[row][column] == '1')
                {
                    ++result;
                    bfs(row, column);
                }
            }
        }

        return result;
    }
};
