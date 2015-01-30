// https://oj.leetcode.com/problems/palindrome-partitioning/

#include "Solution.h"

int main()
{
    const char *testCases[] = { "a" };
    Solution s;

    for (auto testCase : testCases)
    {
        s.partition(testCase);
    }
}
