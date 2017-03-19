#pragma once

class Solution
{
public:
    int minimumTotal(vector<vector<int>> &triangle)
    {
        if (triangle.size() == 0)
        {
            return 0;
        }
        else if (triangle.size() == 1)
        {
            return triangle[0][0];
        }
        else
        {
            vector<vector<int>> selected_paths;
            selected_paths.resize(triangle.size() - 1);

            for (size_t i = 0; i < triangle.size() - 1; ++i)
            {
                selected_paths[i].resize(triangle[i].size());
            }

            selected_paths.emplace_back(triangle.back());

            for (int row = selected_paths.size() - 2; row >= 0; --row)
            {
                for (size_t column = 0; column < selected_paths[row].size(); ++column)
                {
                    selected_paths[row][column] =
                        min(selected_paths[row + 1][column], selected_paths[row + 1][column + 1]) +
                        triangle[row][column];
                }
            }

            return selected_paths[0][0];
        }
    }
};
