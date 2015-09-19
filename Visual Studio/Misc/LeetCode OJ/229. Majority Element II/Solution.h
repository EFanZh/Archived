#pragma once

class Solution
{
public:
    vector<int> majorityElement(const vector<int> &nums)
    {
        map<int, pair<size_t, size_t>> counts;

        for (size_t i = 0; i < nums.size(); ++i)
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

        vector<int> result;

        for (const auto &p : counts)
        {
            if (p.second.second + (nums.size() - p.second.first) > nums.size() ||
                count(nums.cbegin(), nums.cbegin() + p.second.first, p.first) > (p.second.first - p.second.second) / 3)
            {
                result.emplace_back(p.first);
            }
        }

        return result;
    }
};
