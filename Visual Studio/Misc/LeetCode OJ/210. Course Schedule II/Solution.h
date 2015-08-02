#pragma once

class Solution
{
    enum class VisitState : uint8_t
    {
        Unvisited,
        Visiting,
        Visited
    };

    static bool dfs(const vector<vector<size_t>> &g, vector<VisitState> &visited, size_t i, vector<int> &result)
    {
        visited[i] = VisitState::Visiting;

        for (size_t previous : g[i])
        {
            if (visited[previous] == VisitState::Unvisited)
            {
                if (!dfs(g, visited, previous, result))
                {
                    return false;
                }
            }
            else if (visited[previous] == VisitState::Visiting)
            {
                return false;
            }
        }

        result.emplace_back(static_cast<int>(i));

        visited[i] = VisitState::Visited;

        return true;
    }

public:
    vector<int> findOrder(int numCourses, vector<pair<int, int>> &prerequisites)
    {
        vector<vector<size_t>> g(numCourses);

        for (const auto &p : prerequisites)
        {
            g[p.first].emplace_back(p.second);
        }

        vector<VisitState> visited(numCourses, VisitState::Unvisited);
        vector<int> result;

        for (size_t i = 0; i < static_cast<size_t>(numCourses); ++i)
        {
            if (visited[i] == VisitState::Unvisited)
            {
                if (!dfs(g, visited, i, result))
                {
                    return{};
                }
            }
        }

        return result;
    }
};
