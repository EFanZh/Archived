// https://leetcode.com/problems/majority-element-ii/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 1, 2, 3 }, { 0, -1, 2, -1 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.majorityElement(testCase);
    }
}
