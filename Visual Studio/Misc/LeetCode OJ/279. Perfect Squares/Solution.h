#pragma once

class Solution
{
public:
    int numSquares(int n)
    {
        queue<pair<int, pair<int, int>>> q;
        vector<char> visited(n + 1);

        visited[0] = true;

        q.emplace(0, make_pair(0, 0));

        while (!q.empty())
        {
            auto current = q.front();

            q.pop();

            for (int i = static_cast<int>(sqrt(n - current.first)); i >= current.second.first; --i)
            {
                int next = current.first + i * i;

                if (!visited[next])
                {
                    if (next == n)
                    {
                        return current.second.second + 1;
                    }

                    visited[next] = true;
                    q.emplace(next, make_pair(i, current.second.second + 1));
                }
            }
        }

        return -1;
    }
};
