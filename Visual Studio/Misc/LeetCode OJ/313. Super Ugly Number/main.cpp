// https://leetcode.com/problems/super-ugly-number/

#include "Solution.h"

int main()
{
    pair<int, vector<int>> testCases[] = { { 100000, { 7, 19, 29, 37, 41, 47, 53, 59, 61, 79, 83, 89, 101, 103, 109, 127, 131, 137, 139, 157, 167, 179, 181, 199, 211, 229, 233, 239, 241, 251 } } };

    Solution s;

    for (const auto &testCase : testCases)
    {
        s.nthSuperUglyNumber(testCase.first, testCase.second);
    }
}
