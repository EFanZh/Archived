// https://oj.leetcode.com/problems/largest-number/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 1 } };
    Solution s;

    for (auto &testCases:testCases)
    {
        s.largestNumber(testCases);
    }
}
