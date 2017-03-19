#pragma once

class Solution
{
public:
    vector<int> maxSlidingWindow(const vector<int> &nums, int k)
    {
        if (k == 0)
        {
            return {};
        }

        auto window = deque<size_t>();
        auto last = nums.size() - k;
        auto result = vector<int>(nums.size() - k + 1);

        window.emplace_back(k - 1);

        for (auto i = static_cast<size_t>(k - 2); i < static_cast<size_t>(k); --i)
        {
            if (nums[i] > nums[window.front()])
            {
                window.emplace_front(i);
            }
        }

        result.front() = nums[window.front()];

        for (auto i = size_t(0); i < last; ++i)
        {
            if (window.front() <= i)
            {
                window.pop_front();
            }

            while (!window.empty() && nums[window.back()] <= nums[i + k])
            {
                window.pop_back();
            }

            window.emplace_back(i + k);

            result[i + 1] = nums[window.front()];
        }

        return result;
    }
};
