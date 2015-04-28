// https://leetcode.com/problems/trapping-rain-water/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 4, 2, 3 } };
    Solution s;

    for (auto testCase : testCases)
    {
        s.trap(testCase);
    }
}
