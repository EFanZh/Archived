// https://leetcode.com/problems/guess-number-higher-or-lower-ii/

#include "Solution.h"

int main()
{
    const auto testCases = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    Solution s;

    for (auto testCase : testCases)
    {
        cout << s.getMoneyAmount(testCase) << '\n';
    }
}
