#pragma once

class Solution
{
public:
    vector<int> topKFrequent(const vector<int> &nums, int k)
    {
        auto countOf = unordered_map<int, int>();

        for (const auto num : nums)
        {
            ++countOf[num];
        }

        auto v = vector<const pair<const int, int> *>();

        for (const auto &p : countOf)
        {
            v.emplace_back(&p);
        }

        partial_sort(v.begin(),
                     v.begin() + k,
                     v.end(),
                     [](const pair<const int, int> *lhs, const pair<const int, int> *rhs) {
                         return rhs->second < lhs->second;
                     });

        auto result = vector<int>();

        transform(v.begin(), v.begin() + k, back_inserter(result), [](const pair<const int, int> *x) {
            return x->first;
        });

        return result;
    }
};
