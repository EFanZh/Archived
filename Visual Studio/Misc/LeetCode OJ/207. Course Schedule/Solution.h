#pragma once

class Solution
{
    enum class VisitState : uint8_t
    {
        Unvisited = 0,
        Visiting,
        Visited
    };

    static bool dfs(const vector<unordered_set<size_t>> &g, vector<VisitState> &visited, size_t i)
    {
        visited[i] = VisitState::Visiting;

        for (size_t next : g[i])
        {
            if (visited[next] == VisitState::Unvisited)
            {
                if (!dfs(g, visited, next))
                {
                    return false;
                }
            }
            else if (visited[next] == VisitState::Visiting)
            {
                return false;
            }
        }

        visited[i] = VisitState::Visited;

        return true;
    }

public:
    bool canFinish(int numCourses, vector<pair<int, int>> &prerequisites)
    {
        vector<unordered_set<size_t>> g(numCourses);

        for (const auto &p : prerequisites)
        {
            g[p.second].emplace(p.first);
        }

        vector<VisitState> visited(numCourses);

        for (size_t i = 0; i < g.size(); ++i)
        {
            if (visited[i] == VisitState::Unvisited)
            {
                if (!dfs(g, visited, i))
                {
                    return false;
                }
            }
        }

        return true;
    }
};
