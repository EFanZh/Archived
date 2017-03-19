#pragma once

class Solution
{
    struct Environment
    {
        const vector<vector<int>> &matrix;
        size_t rows;
        size_t columns;
        vector<int> visited;
    };

    static void dfs(Environment &e, size_t position, size_t x, size_t y)
    {
        // Visiting.
        e.visited[position] = 1;

        if (x > 0 && e.matrix[y][x - 1] > e.matrix[y][x])
        {
            if (e.visited[position - 1] == 0)
            {
                dfs(e, position - 1, x - 1, y);
            }

            e.visited[position] = e.visited[position - 1] + 1;
        }

        if (y > 0 && e.matrix[y - 1][x] > e.matrix[y][x])
        {
            if (e.visited[position - e.columns] == 0)
            {
                dfs(e, position - e.columns, x, y - 1);
            }

            e.visited[position] = max(e.visited[position], e.visited[position - e.columns] + 1);
        }

        if (x < e.columns - 1 && e.matrix[y][x + 1] > e.matrix[y][x])
        {
            if (e.visited[position + 1] == 0)
            {
                dfs(e, position + 1, x + 1, y);
            }

            e.visited[position] = max(e.visited[position], e.visited[position + 1] + 1);
        }

        if (y < e.rows - 1 && e.matrix[y + 1][x] > e.matrix[y][x])
        {
            if (e.visited[position + e.columns] == 0)
            {
                dfs(e, position + e.columns, x, y + 1);
            }

            e.visited[position] = max(e.visited[position], e.visited[position + e.columns] + 1);
        }
    }

public:
    int longestIncreasingPath(const vector<vector<int>> &matrix)
    {
        if (matrix.empty() || matrix.front().empty())
        {
            return 0;
        }

        Environment environment = { matrix, matrix.size(), matrix.front().size(),
                                    vector<int>(matrix.size() * matrix.front().size()) };
        auto result = 0;

        for (size_t i = 0; i < environment.rows; ++i)
        {
            for (size_t j = 0; j < environment.columns; ++j)
            {
                const auto k = environment.columns * i + j;

                // If not visited.
                if (environment.visited[k] == 0)
                {
                    dfs(environment, k, j, i);
                }

                result = max(result, environment.visited[k]);
            }
        }

        return result;
    }
};
