// https://leetcode.com/problems/count-of-smaller-numbers-after-self/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 2, 0, 1 }, { -1, -1 }, { 5, 2, 6, 1 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.countSmaller(testCase);
    }
}
