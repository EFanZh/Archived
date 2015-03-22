#pragma once

class Solution
{
public:
    uint32_t reverseBits(uint32_t n)
    {
        uint32_t result = 0;

        for (size_t i = 0; i < 32; ++i)
        {
            if (n & (1 << i))
            {
                result |= (1 << (31 - i));
            }
        }

        return result;
    }
};
