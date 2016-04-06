// https://leetcode.com/problems/burst-balloons/

#include "Solution.h"

int main()
{
    vector<int> testCases[] = {
        { 3, 1, 5, 8 },
        { 9, 76, 64, 21, 97, 60 },
        { 7, 9, 8, 0, 7, 1, 3, 5, 5, 2, 3 },
        { 2, 4, 8, 4, 0, 7, 8, 9, 1, 2, 4, 7, 1, 7, 3, 6 }
    };
    Solution s;

    for (auto &testCase : testCases)
    {
        cout << s.maxCoins(testCase) << '\n';
    }
}
