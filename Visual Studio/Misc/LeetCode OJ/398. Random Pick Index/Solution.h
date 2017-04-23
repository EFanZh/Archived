#pragma once

class Solution
{
    vector<pair<int, int>> indexOf;
    default_random_engine re;

public:
    Solution(const vector<int> &nums)
    {
        auto length = nums.size();

        indexOf.reserve(length);

        for (auto i = decltype(length)(0); i < length; ++i)
        {
            indexOf.emplace_back(nums[i], static_cast<int>(i));
        }

        sort(indexOf.begin(), indexOf.end(), [](const pair<int, int> &lhs, const pair<int, int> &rhs) {
            return lhs.first < rhs.first;
        });
    }

    int pick(int target)
    {
        const auto its =
            equal_range(indexOf.cbegin(),
                        indexOf.cend(),
                        make_pair(target, 0),
                        [](const pair<int, int> &lhs, const pair<int, int> &rhs) { return lhs.first < rhs.first; });

        const auto size = its.second - its.first;

        return its.first[uniform_int_distribution<remove_const<decltype(size)>::type>(0, size - 1)(re)].second;
    }
};

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(nums);
 * int param_1 = obj.pick(target);
 */
