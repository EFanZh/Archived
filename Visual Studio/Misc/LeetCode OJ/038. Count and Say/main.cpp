// https://oj.leetcode.com/problems/count-and-say/

#include "Solution.h"

int main()
{
    auto testCases = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    Solution s;

    for (auto testCase : testCases)
    {
        s.countAndSay(testCase);
    }
}
