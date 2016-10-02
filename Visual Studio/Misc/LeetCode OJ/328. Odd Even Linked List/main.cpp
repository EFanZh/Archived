// https://leetcode.com/problems/odd-even-linked-list/

#include "Solution.h"

int main()
{
    initializer_list<int> testCases[] = { {}, { 1, 2 }, { 1, 2, 3 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4, 5 } };
    Pool<ListNode> pool;
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.oddEvenList(MakeList(pool, testCase));
    }
}
