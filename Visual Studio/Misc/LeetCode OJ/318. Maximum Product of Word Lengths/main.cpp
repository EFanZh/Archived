// https://leetcode.com/problems/maximum-product-of-word-lengths/

#include "Solution.h"

int main()
{
    const vector<string> testCases[] = { { "abcw", "baz", "foo", "bar", "xtfn", "abcdef" } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        s.maxProduct(testCase);
    }
}
