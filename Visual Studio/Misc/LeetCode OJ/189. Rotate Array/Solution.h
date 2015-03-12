#pragma once

class Solution
{
public:
    void rotate(int nums[], int n, int k)
    {
        k %= n;
        reverse(nums, nums + n);
        reverse(nums, nums + k);
        reverse(nums + k, nums + n);
    }
};
