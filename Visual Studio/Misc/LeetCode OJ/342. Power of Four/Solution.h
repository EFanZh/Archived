#pragma once

class Solution
{
public:
    bool isPowerOfFour(int num)
    {
        return (num & (num - 1)) == 0 && (num - 1) % 3 == 0;
    }
};
