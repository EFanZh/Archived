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

        deque<size_t> window;
        size_t last = nums.size() - k;
        vector<int> result(nums.size() - k + 1);

        window.emplace_back(k - 1);

        for (size_t i = k - 2; i < k; --i)
        {
            if (nums[i] > nums[window.front()])
            {
                window.emplace_front(i);
            }
        }

        result.front() = nums[window.front()];

        for (size_t i = 0; i < last; ++i)
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
