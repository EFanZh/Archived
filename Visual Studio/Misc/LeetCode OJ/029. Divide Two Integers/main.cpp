// https://oj.leetcode.com/problems/divide-two-integers/

#include "Solution.h"

int main()
{
    const pair<int, int> testCases[] = { { numeric_limits<int>::min(), -1 }, { numeric_limits<int>::min(), 1 } };
    auto s = Solution();

    for (const auto &testCase : testCases)
    {
        s.divide(testCase.first, testCase.second);
    }
}
