// https://oj.leetcode.com/problems/reverse-linked-list-ii/

#include "Solution.h"

int main()
{
    tuple<initializer_list<int>, int, int> testCases[] = { make_tuple<initializer_list<int>>({ 1, 2, 3 }, 1, 3) };
    Solution s;

    for (auto &testCase : testCases)
    {
        vector<unique_ptr<ListNode>> pool;

        s.reverseBetween(MakeList(pool, get<0>(testCase)), get<1>(testCase), get<2>(testCase));
    }
}
