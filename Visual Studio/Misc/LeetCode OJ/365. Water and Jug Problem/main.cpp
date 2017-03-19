// https://leetcode.com/problems/water-and-jug-problem/

#include "Solution.h"

int main()
{
    const tuple<int, int, int> testCases[] = { make_tuple(1, 1, 0),
                                               make_tuple(104579, 104593, 12444),
                                               make_tuple(1, 2, 3),
                                               make_tuple(3, 5, 4),
                                               make_tuple(2, 6, 5) };
    Solution s;

    for (const auto testCase : testCases)
    {
        s.canMeasureWater(get<0>(testCase), get<1>(testCase), get<2>(testCase));
    }
}
