// https://leetcode.com/problems/subsets-ii/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 1, 2, 3, 4, 5, 6, 7, 8, 10, 0 } };
    Solution s;

    for (auto &testCase : testCases)
    {
        s.subsetsWithDup(testCase);
    }
}
