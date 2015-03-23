// https://leetcode.com/problems/partition-list/

#include "Solution.h"

int main()
{
    pair<initializer_list<int>, int> testCases[] = { { { 2, 1 }, 2 } };
    Solution s;

    for (auto testCase : testCases)
    {
        vector<unique_ptr<ListNode>> pool;

        s.partition(MakeList(pool, testCase.first), testCase.second);
    }
}
