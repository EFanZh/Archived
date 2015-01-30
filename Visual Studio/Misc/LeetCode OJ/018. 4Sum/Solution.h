#pragma once

class Solution
{
public:
    vector<vector<int>> fourSum(vector<int> &num, int target)
    {
        if (num.size() < 4)
        {
            return {};
        }

        sort(num.begin(), num.end());

        unordered_map<int, vector<pair<size_t, size_t>>> twoSums;

        for (size_t i = 0; i < num.size() - 1; ++i)
        {
            for (size_t j = i + 1; j < num.size(); ++j)
            {
                twoSums[num[i] + num[j]].emplace_back(i, j);
            }
        }

        vector<vector<int>> result;

        for (auto &p : twoSums)
        {
            if (p.first <= target / 2)
            {
                auto it = twoSums.find(target - p.first);

                if (it != twoSums.cend())
                {
                    set<tuple<int, int, int, int>> tempResult;

                    for (auto &x : p.second)
                    {
                        for (auto &y : it->second)
                        {
                            if (x.second < y.first)
                            {
                                tempResult.insert(make_tuple(num[x.first], num[x.second], num[y.first], num[y.second]));
                            }
                        }
                    }

                    for (auto &k : tempResult)
                    {
                        result.push_back({ get<0>(k), get<1>(k), get<2>(k), get<3>(k) });
                    }
                }
            }
        }

        return result;
    }
};
