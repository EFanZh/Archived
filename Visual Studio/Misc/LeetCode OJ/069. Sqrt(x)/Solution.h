#pragma once

class Solution
{
    int findSqrt(int x, int guess)
    {
        int other = x / guess;

        if (guess < other)
        {
            if (x / (guess + 1) < guess + 1)
            {
                return guess;
            }
        }
        else if (guess == other)
        {
            return guess;
        }

        return findSqrt(x, (guess + other) / 2);
    }

public:
    int sqrt(int x)
    {
        return x <= 1 ? x : findSqrt(x, x / 2);
    }
};
