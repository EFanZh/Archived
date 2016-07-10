// https://leetcode.com/problems/create-maximum-number/

#include "Solution.h"

struct TestCase
{
    vector<int> nums1;
    vector<int> nums2;
    int k;
};

int main()
{
    TestCase testCases[] =
    {
        { { 2, 5, 6, 4, 4, 0 }, { 7, 3, 8, 0, 6, 5, 7, 6, 2 }, 15 },
        { { 8, 9 }, { 3, 9 }, 3 },
        { { 3, 9 }, { 8, 9 }, 3 },
        { { 6, 7 }, { 6, 0, 4 }, 5 },
        { { 3, 4, 6, 5 }, { 9, 1, 2, 5, 8, 3 }, 5 }
    };

    Solution s;

    for (const auto &testCase : testCases)
    {
        s.maxNumber(testCase.nums1, testCase.nums2, testCase.k);
    }
}
