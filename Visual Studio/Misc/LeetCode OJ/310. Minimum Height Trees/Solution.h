#pragma once

class Solution
{
public:
    vector<int> findMinHeightTrees(int n, const vector<pair<int, int>> &edges)
    {
        if (n == 0)
        {
            return{};
        }
        else if (n == 1)
        {
            return{ 0 };
        }
        else if (n == 2)
        {
            return{ 0, 1 };
        }

        auto graph = vector<vector<int>>(n);

        for (const auto &edge : edges)
        {
            graph[edge.first].emplace_back(edge.second);
            graph[edge.second].emplace_back(edge.first);
        }

        auto current = vector<int>();
        auto visited = vector<char>(n, false);

        for (auto i = 0; i < n; ++i)
        {
            if (graph[i].size() == 1)
            {
                current.emplace_back(i);
                visited[i] = true;
            }
        }

        auto next = vector<int>();

        for (;;)
        {
            for (const auto node : current)
            {
                for (const auto neighbor : graph[node])
                {
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        next.emplace_back(neighbor);
                    }
                }
            }

            if (next.size() == 0)
            {
                return current;
            }
            else
            {
                current = next;
                next.clear();
            }
        }
    }
};
