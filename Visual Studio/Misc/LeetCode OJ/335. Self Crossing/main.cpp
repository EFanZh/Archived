// https://leetcode.com/problems/self-crossing/

#include "Solution.h"

int main()
{
    const vector<int> testCases[] = { { 2, 1, 1, 2 }, { 1, 2, 3, 4 }, { 1, 1, 1, 1 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        cout << s.isSelfCrossing(testCase);
    }
}
