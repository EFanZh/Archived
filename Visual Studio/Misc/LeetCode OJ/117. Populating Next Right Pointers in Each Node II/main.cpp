// https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii/

#include "Solution.h"

int main()
{
    const char *testCases[] = { "{1,2,3,4,5}", "{1,2,3,4,#,#,5}" };
    Solution s;

    for (auto testCase : testCases)
    {
        stringstream input(testCase);
        vector<unique_ptr<TreeLinkNode>> pool;

        s.connect(MakeTree(pool, input));
    }
}
