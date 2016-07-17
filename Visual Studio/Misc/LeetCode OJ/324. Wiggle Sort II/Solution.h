#pragma once

class Solution
{
    static int getMedian(vector<int> &nums)
    {
        const auto it = nums.begin() + nums.size() / 2;

        nth_element(nums.begin(), it, nums.end());

        return *it;
    }

public:
    void wiggleSort(vector<int> &nums)
    {
        if (nums.size() < 2)
        {
            return;
        }

        const auto median = getMedian(nums);
        auto i = vector<int>::size_type(0);
        auto k = vector<int>::size_type(nums.size());

        const auto getNum = [&](vector<int>::size_type i) -> int &
        {
            const auto half = (nums.size() + 1) / 2;

            return nums[i < half ? 2 * (half - 1 - i) : 2 * (nums.size() - 1 - i) + 1];
        };

        for (auto j = vector<int>::size_type(0); j < k;)
        {
            auto &current = getNum(j);

            if (current < median)
            {
                swap(current, getNum(i));

                ++i;
                ++j;
            }
            else if (current == median)
            {
                ++j;
            }
            else
            {
                --k;

                swap(current, getNum(k));
            }
        }
    }
};
