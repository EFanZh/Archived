#include "Solution.h"

int main()
{
    initializer_list<int> testCases[] = { { 1 }, { 1, 1 } };
    Solution s;

    for (auto testCase : testCases)
    {
        vector<unique_ptr<ListNode>> pool;

        s.deleteDuplicates(MakeList(pool, testCase));
    }
}
