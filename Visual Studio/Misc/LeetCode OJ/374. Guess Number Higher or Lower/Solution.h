#pragma once

// Forward declaration of guess API.
// @param num, your guess
// @return -1 if my number is lower, 1 if my number is higher, otherwise return 0
int guess(int num);

class Solution
{
public:
    int guessNumber(int n)
    {
        auto left = 1u;
        auto right = static_cast<unsigned int>(n);

        for (;;)
        {
            const auto middle = (left + right) / 2; // Don't worry about overflowing.

            switch (guess(middle))
            {
                case -1:
                    right = middle - 1;
                    break;

                case 0:
                    return static_cast<int>(middle);

                default:
                    left = middle + 1;
                    break;
            }
        }
    }
};
