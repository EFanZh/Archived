#pragma once

class Solution
{
public:
    vector<int> twoSum(const vector<int> &numbers, int target)
    {
        const auto length = numbers.size();

        for (auto i = size_t(1); i < length; ++i)
        {
            auto left = size_t(0);
            auto count = i;
            const auto find = target - numbers[i];

            while (count > 0)
            {
                auto leftSize = count / 2;
                auto middle = left + leftSize;

                if (numbers[left + leftSize] < find)
                {
                    left = middle + 1;
                    count -= leftSize + 1;
                }
                else
                {
                    count = leftSize;
                }
            }

            if (left < i && numbers[left] == find)
            {
                return { static_cast<int>(left) + 1, static_cast<int>(i) + 1 };
            }
        }

        return {};
    }
};
