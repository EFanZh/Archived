#pragma once

class Solution
{
public:
    int integerBreak(int n)
    {
        static const int powersOf3[] = { 1, 3, 9, 27, 81, 243, 729, 2187, 6561, 19683, 59049, 177147, 531441, 1594323, 4782969, 14348907, 43046721, 129140163, 387420489, 1162261467 };

        if (n < 4)
        {
            return n - 1;
        }
        else
        {
            // https://oeis.org/A000792
            switch (n % 3)
            {
                case 0:
                    return powersOf3[n / 3];

                case 1:
                    return powersOf3[n / 3 - 1] * 4;

                default:
                    return powersOf3[n / 3] * 2;
            }
        }
    }
};
