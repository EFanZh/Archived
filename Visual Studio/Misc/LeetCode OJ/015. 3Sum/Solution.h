#pragma once

class Solution
{
public:
    vector<vector<int>> threeSum(vector<int> &num)
    {
        vector<vector<int>> result;

        if (num.size() >= 3)
        {
            vector<pair<int, vector<int>>> value_to_indexes;

            sort(num.begin(), num.end());
            for (size_t i = 0; i < num.size();)
            {
                int value = num[i];
                value_to_indexes.emplace_back(value, vector<int>({ i }));
                auto &v = value_to_indexes.back().second;

                ++i;
                while (i < num.size() && num[i] == value)
                {
                    v.emplace_back(static_cast<int>(i));
                    ++i;
                }
            }

            const auto i_max = upper_bound(num.cbegin(), num.cend(), 0) - num.cbegin();

            for (auto i = 0; i != i_max; ++i)
            {
                if (result.empty() || num[i] != result.back()[0])
                {
                    const auto j_max = upper_bound(num.cbegin() + i + 1, num.cend(), -num[i] / 2) - num.cbegin();
                    for (int j = i + 1; j != j_max; ++j)
                    {
                        if (result.empty() || num[i] != result.back()[0] || result.back()[1] != num[j])
                        {
                            auto it_value_to_ids =
                                lower_bound(value_to_indexes.begin(),
                                            value_to_indexes.end(),
                                            -num[i] - num[j],
                                            [](const pair<int, vector<int>> &p, int n) { return p.first < n; });
                            if (it_value_to_ids != value_to_indexes.cend() &&
                                it_value_to_ids->first == -num[i] - num[j])
                            {
                                for (auto k : it_value_to_ids->second)
                                {
                                    if (k > j)
                                    {
                                        result.push_back({ num[i], num[j], num[k] });
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return result;
    }
};
