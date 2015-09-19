#pragma once

class Solution
{
public:
    vector<int> singleNumber(const vector<int> &nums)
    {
        int aXorB = accumulate(nums.cbegin(), nums.cend(), 0, bit_xor<int>());
        int lastDifferentBit = ((aXorB ^ (aXorB - 1)) + 1) >> 1;
        int theOneWithLastOneBit = 0;
        int theOneWithoutLastOneBit = 0;

        for (int num : nums)
        {
            if (num & lastDifferentBit)
            {
                theOneWithLastOneBit ^= num;
            }
            else
            {
                theOneWithoutLastOneBit ^= num;
            }
        }

        return{ theOneWithLastOneBit, theOneWithoutLastOneBit };
    }
};
