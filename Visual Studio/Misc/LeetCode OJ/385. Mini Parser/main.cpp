// https://leetcode.com/problems/mini-parser/

#include "Solution.h"

int main()
{
    string testCases[] = { "[123,456,[788,799,833],[[]],10,[]]", "-3", "[123,[456,[789]]]" };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.deserialize(testCase);
    }
}
