#pragma once

class Solution
{
    static double powPositive(double x, int n)
    {
        if (n == 0)
        {
            return 1;
        }
        else
        {
            double k = powPositive(x, n / 2);

            return n % 2 == 0 ? k * k : k * k * x;
        }
    }

public:
    double pow(double x, int n)
    {
        return n < 0 ? 1 / powPositive(x, -n) : powPositive(x, -n);
    }
};
