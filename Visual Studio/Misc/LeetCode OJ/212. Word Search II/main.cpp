// https://leetcode.com/problems/word-search-ii/

#include "Solution.h"

int main()
{
    pair<vector<vector<char>>, vector<string>> testCases[] =
    {
        { { { 'a' } }, { "a" } },
        { { { 'a', 'a' } }, { "a" } }
    };
    Solution s;

    for (auto &testCase : testCases)
    {
        s.findWords(testCase.first, testCase.second);
    }
}
