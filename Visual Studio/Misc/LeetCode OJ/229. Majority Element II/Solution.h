#pragma once

class Solution
{
public:
    vector<int> majorityElement(const vector<int> &nums)
    {
        auto counts = map<int, pair<size_t, size_t>>();

        for (auto i = size_t(0); i < nums.size(); ++i)
        {
            auto &count = counts[nums[i]];

            if (count.second == 0)
            {
                count.first = i;
            }

            count.second += 2;

            for (auto it = counts.begin(); it != counts.end();)
            {
                if (it->first != nums[i])
                {
                    --it->second.second;
                }

                if (it->second.second == 0)
                {
                    it = counts.erase(it);
                }
                else
                {
                    ++it;
                }
            }
        }

        auto result = vector<int>();

        for (const auto &p : counts)
        {
            if (p.second.second + (nums.size() - p.second.first) > nums.size() ||
                static_cast<size_t>(count(nums.cbegin(), nums.cbegin() + p.second.first, p.first)) >
                    (p.second.first - p.second.second) / 3)
            {
                result.emplace_back(static_cast<int>(p.first));
            }
        }

        return result;
    }
};
