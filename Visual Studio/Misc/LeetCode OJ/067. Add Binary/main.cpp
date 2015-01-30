// https://oj.leetcode.com/problems/add-binary/

#include "Solution.h"

int main()
{
    pair<const char *, const char *> testCases[] = { { "11", "1" } };
    Solution s;

    for (auto testCase : testCases)
    {
        s.addBinary(testCase.first, testCase.second);
    }
}
