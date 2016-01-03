// https://leetcode.com/problems/additive-number/

#include "Solution.h"

int main()
{
    const char *testCases[] = { "12012122436", "199100199", "8917", "101" };
    Solution s;

    for (const auto testCase : testCases)
    {
        s.isAdditiveNumber(testCase);
    }
}
