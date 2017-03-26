#pragma once

class Solution
{
public:
    int candy(vector<int> &ratings)
    {
        queue<pair<size_t, int>> q;
        vector<bool> peaks(ratings.size(), false);

        for (size_t i = 0; i < ratings.size(); ++i)
        {
            if ((i == 0 || ratings[i] <= ratings[i - 1]) && (i == ratings.size() - 1 || ratings[i] <= ratings[i + 1]))
            {
                q.emplace(i, 1);
            }
            else if (i > 0 && i < ratings.size() - 1 && ratings[i] > ratings[i - 1] && ratings[i] > ratings[i + 1])
            {
                peaks[i] = true;
            }
        }

        size_t result = 0;

        while (!q.empty())
        {
            auto item = q.front();

            q.pop();

            result += item.second;

            for (auto next : { item.first - 1, item.first + 1 })
            {
                if (next < ratings.size() && ratings[next] > ratings[item.first])
                {
                    if (peaks[next])
                    {
                        peaks[next] = false;
                    }
                    else
                    {
                        q.emplace(next, item.second + 1);
                    }
                }
            }
        }

        return static_cast<int>(result);
    }
};
