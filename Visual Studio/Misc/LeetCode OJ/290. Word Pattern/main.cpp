// https://leetcode.com/problems/word-pattern/

#include "Solution.h"

int main()
{
    pair<const char *, const char *> testCases[] = { { "aaa", "aa aa aa aa" }, { "abba", "dog cat cat dog" } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.wordPattern(testCase.first, testCase.second);
    }
}
