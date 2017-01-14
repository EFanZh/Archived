#pragma once

class Solution
{
public:
    bool isPerfectSquare(int num)
    {
        auto left = 0;
        auto right = 46340;

        for (;;)
        {
            const auto middle = (left + right) >> 1; // No need to worry about overflow.
            const auto middleSquared = middle * middle;

            if (middleSquared < num)
            {
                left = middle + 1;
            }
            else
            {
                right = middle;
            }

            if (left == right)
            {
                return left * left == num;
            }
        }
    }
};
