#pragma once

class Solution
{
public:
    int calculateMinimumHP(vector<vector<int>> &dungeon)
    {
        size_t rows = dungeon.size();
        size_t columns = dungeon.front().size();
        vector<int> cache(columns * rows, numeric_limits<int>::max());
        queue<pair<size_t, size_t>> q;
        auto getCache = [&](size_t row, size_t column) -> int &
        {
            return cache[columns * row + column];
        };

        cache.back() = 1;
        q.emplace(rows - 1, columns - 1);

        while (!q.empty())
        {
            auto current = q.front();

            q.pop();

            if (current.first > 0)
            {
                if (getCache(current.first - 1, current.second) == numeric_limits<int>::max())
                {
                    q.emplace(current.first - 1, current.second);
                }

                auto &next = getCache(current.first - 1, current.second);

                next = min(next, max(1, getCache(current.first, current.second) - dungeon[current.first][current.second]));
            }

            if (current.second > 0)
            {
                if (getCache(current.first, current.second - 1) == numeric_limits<int>::max())
                {
                    q.emplace(current.first, current.second - 1);
                }

                auto &next = getCache(current.first, current.second - 1);

                next = min(next, max(1, getCache(current.first, current.second) - dungeon[current.first][current.second]));
            }
        }

        return max(1, cache.front() - dungeon.front().front());
    }
};
