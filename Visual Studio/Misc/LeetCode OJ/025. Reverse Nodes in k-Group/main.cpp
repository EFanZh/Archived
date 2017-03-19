// https://oj.leetcode.com/problems/reverse-nodes-in-k-group/

#include "Solution.h"

int main()
{
    pair<initializer_list<int>, int> testCases[] = { { { 1, 2, 3 }, 3 }, { { 1, 2, 3, 4 }, 2 } };
    Solution s;

    for (auto &testCase : testCases)
    {
        vector<unique_ptr<ListNode>> pool;

        s.reverseKGroup(MakeList(pool, testCase.first), testCase.second);
    }
}
