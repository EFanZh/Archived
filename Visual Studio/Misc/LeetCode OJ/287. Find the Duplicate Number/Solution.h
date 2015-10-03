#pragma once

class Solution
{
public:
    int findDuplicate(const vector<int> &nums)
    {
        int slow = nums.back();
        auto next = [&](int x) { return nums[x - 1]; };
        int fast = next(slow);

        while (slow != fast)
        {
            slow = next(slow);
            fast = next(next(fast));
        }

        for (slow = static_cast<int>(nums.size()); slow != fast; slow = next(slow))
        {
            fast = next(fast);
        }

        return slow;
    }
};
