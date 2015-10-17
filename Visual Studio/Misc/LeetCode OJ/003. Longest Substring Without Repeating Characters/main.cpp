// https://leetcode.com/problems/longest-substring-without-repeating-characters/

#include "Solution.h"

int main()
{
    const char *testCases[] = { "cdd", "aab" };
    Solution s;

    for (auto testCase : testCases)
    {
        s.lengthOfLongestSubstring(testCase);
    }
}
