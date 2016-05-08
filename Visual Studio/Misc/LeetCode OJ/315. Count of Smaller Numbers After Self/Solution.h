#pragma once

class Solution
{
public:
    vector<int> countSmaller(const vector<int> &nums)
    {
        auto result = vector<int>(nums.size());
        auto sequences = vector<vector<vector<int>::size_type>>();

        for (auto i = nums.size() - 1; i < nums.size(); --i)
        {
            auto it = lower_bound(sequences.begin(),
                sequences.end(),
                nums[i],
                [&](const vector<vector<int>::size_type> &lhs, int rhs)
            {
                return nums[lhs.back()] > rhs;
            });

            if (it == sequences.end())
            {
                sequences.push_back({ i });
                result[i] = 0;
            }
            else
            {
                if (nums[it->back()] == nums[i])
                {
                    result[i] = result[it->back()];
                    it->back() = i;
                }
                else
                {
                    result[i] = static_cast<int>(it->back() - i + result[it->back()]);
                    it->emplace_back(i);
                }
            }
        }

        return result;
    }
};
