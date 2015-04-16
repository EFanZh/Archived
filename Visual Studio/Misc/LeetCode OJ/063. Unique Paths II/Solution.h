#pragma once

class Solution
{
    template<class T>
    static int uniquePathsWithObstaclesHelper(size_t width, size_t height, T isObstacle)
    {
        if (isObstacle(0, 0) || isObstacle(width - 1, height - 1))
        {
            return 0;
        }

        if (height == 1)
        {
            return 1;
        }

        vector<int> cache(width);

        cache.back() = 1;

        // Bottom triangle.
        for (size_t i = 1; i < width; ++i)
        {
            size_t x0 = width - 1 - i;
            size_t y0 = height - 1;

            if (isObstacle(x0, y0))
            {
                cache[x0] = 0;
            }
            else
            {
                cache[x0] += cache[x0 + 1];
            }

            for (size_t j = 1; j < i; j++)
            {
                size_t x = x0 + j;

                if (isObstacle(x, y0 - j))
                {
                    cache[x] = 0;
                }
                else
                {
                    cache[x] += cache[x + 1];
                }
            }

            if (isObstacle(width - 1, height - 1 - i))
            {
                cache[width - 1] = 0;
            }
        }

        // Middle parallelogram.
        for (size_t i = 0; i < height - width; ++i)
        {
            for (size_t j = 0; j < width - 1; ++j)
            {
                if (isObstacle(j, height - 2 - i - j))
                {
                    cache[j] = 0;
                }
                else
                {
                    cache[j] += cache[j + 1];
                }
            }

            if (isObstacle(width - 1, height - width - 1 - i))
            {
                cache[width - 1] = 0;
            }
        }

        // Top triangle.
        for (size_t i = 0; i < width - 1; ++i)
        {
            for (size_t j = 0; j < width - 1 - i; ++j)
            {
                if (isObstacle(j, width - 2 - i - j))
                {
                    cache[j] = 0;
                }
                else
                {
                    cache[j] += cache[j + 1];
                }
            }

            if (isObstacle(width - 1 - i, 0))
            {
                cache[width - 1 - i] = 0;
            }
        }

        return cache.front();
    }

public:
    int uniquePathsWithObstacles(vector<vector<int>> &obstacleGrid)
    {
        if (obstacleGrid.front().size() < obstacleGrid.size())
        {
            return uniquePathsWithObstaclesHelper(obstacleGrid.front().size(),
                                                  obstacleGrid.size(),
                                                  [&](size_t x, size_t y) { return obstacleGrid[y][x] != 0; });
        }
        else
        {
            return uniquePathsWithObstaclesHelper(obstacleGrid.size(),
                                                  obstacleGrid.front().size(),
                                                  [&](size_t x, size_t y) { return obstacleGrid[x][y] != 0; });
        }
    }
};
