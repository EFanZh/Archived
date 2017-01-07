// https://leetcode.com/problems/russian-doll-envelopes/

#include "Solution.h"

int main()
{
    vector<pair<int, int>> testCases[] = { { { 4, 5 }, { 4, 6 }, { 6, 7 }, { 2, 3 }, { 1, 1 } },
                                           { { 5, 4 }, { 6, 4 }, { 6, 7 }, { 2, 3 } } };
    Solution s;

    for (const auto testCase : testCases)
    {
        s.maxEnvelopes(testCase);
    }
}
