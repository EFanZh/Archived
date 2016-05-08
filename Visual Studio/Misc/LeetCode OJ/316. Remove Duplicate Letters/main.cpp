// https://leetcode.com/problems/remove-duplicate-letters/

#include "Solution.h"

int main()
{
    string testCases[] = { "abacb", "bcabc", "bbcaac", "cbacdcbc", "rusrbofeggbbkyuyjsrzornpdguwzizqszpbicdquakqws" };
    Solution s;

    for (auto testCase : testCases)
    {
        s.removeDuplicateLetters(testCase);
    }
}
