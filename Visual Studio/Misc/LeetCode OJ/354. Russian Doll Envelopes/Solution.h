#pragma once

class Solution
{
public:
    int maxEnvelopes(const vector<pair<int, int>> &envelopes)
    {
        auto sortedEnvelopes = envelopes;

        sort(sortedEnvelopes.begin(), sortedEnvelopes.end(), [](const pair<int, int> &lhs, const pair<int, int> &rhs) {
            return lhs.first < rhs.first || (lhs.first == rhs.first && lhs.second > rhs.second);
        });

        auto cache = vector<int>(envelopes.size());
        auto first = cache.begin();
        auto last = first;

        for (const auto &p : sortedEnvelopes)
        {
            const auto it = lower_bound(first, last, p.second);

            if (it == last)
            {
                ++last;
            }

            *it = p.second;
        }

        return static_cast<int>(last - first);
    }
};
