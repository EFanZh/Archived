#pragma once

class Solution
{
    static int getTreeHeight(map<pair<int, int>, int> &heights, const vector<vector<int>> &graph,
                             const pair<int, int> &root)
    {
        auto it = heights.find(root);

        if (it != heights.cend())
        {
            return it->second;
        }

        auto maxHeight = 0;

        for (const auto &next : graph[root.second])
        {
            if (next != root.first)
            {
                maxHeight = max(maxHeight, getTreeHeight(heights, graph, { root.second, next }));
            }
        }

        ++maxHeight;

        heights.emplace(root, maxHeight);

        return maxHeight;
    }

public:
    vector<int> findMinHeightTrees(int n, const vector<pair<int, int>> &edges)
    {
        auto graph = vector<vector<int>>(n);

        for (const auto &edge : edges)
        {
            graph[edge.first].emplace_back(edge.second);
            graph[edge.second].emplace_back(edge.first);
        }

        auto heights = map<pair<int, int>, int>();

        for (const auto &edge : edges)
        {
            const auto height1 = getTreeHeight(heights, graph, { edge.second, edge.first });
            const auto height2 = getTreeHeight(heights, graph, edge);

            if (height1 == height2 + 1)
            {
                return { edge.first };
            }
            else if (height1 == height2)
            {
                return { edge.first, edge.second };
            }
            else if (height1 + 1 == height2)
            {
                return { edge.second };
            }
        }

        return { 0 };
    }
};
