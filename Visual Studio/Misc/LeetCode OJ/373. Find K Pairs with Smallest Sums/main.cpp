// https://leetcode.com/problems/find-k-pairs-with-smallest-sums/

#include "Solution.h"

int main()
{
    tuple<vector<int>, vector<int>, int> testCases[] = { { { 1, 1, 2 }, { 1, 2, 3 }, 9 },
                                                         { { 1, 7, 11 }, { 2, 4, 6 }, 9 } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.kSmallestPairs(get<0>(testCase), get<1>(testCase), get<2>(testCase));
    }
}
