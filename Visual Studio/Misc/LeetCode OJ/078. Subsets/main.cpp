// https://leetcode.com/problems/subsets/

#include "Solution.h"
#include "Solution2.h"

int main()
{
    vector<int> testCases[] = { { 1, 2, 3, 4, 5, 6, 7, 8 } };
    Solution s1;
    Solution2 s2;

    for (auto &testCase : testCases)
    {
        s1.subsets(testCase);
        s2.subsets(testCase);
    }
}
