// https://leetcode.com/problems/palindrome-pairs/

#include "Solution.h"

int main()
{
    const vector<string> testCases[] = {
        { "a", "" },
        { "bat", "tab", "cat" },
        { "abcd", "dcba", "lls", "s", "sssll" },
        { "abcd", "dcba" }
    };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.palindromePairs(testCase);
    }
}
