#pragma once

class Solution
{
public:
    int addDigits(int num)
    {
        // https://en.wikipedia.org/wiki/Digital_root#Congruence_formula
        return 1 + (num - 1) % 9;
    }
};
