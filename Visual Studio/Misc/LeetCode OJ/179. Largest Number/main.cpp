// https://oj.leetcode.com/problems/largest-number/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 1 } };
    auto s = Solution();

    for (auto &testCase : testCases)
    {
        s.largestNumber(testCase);
    }
}
