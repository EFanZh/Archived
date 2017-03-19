#include "Solution.h"

int main()
{
    vector<int> testCases[] = { { 4, 5, 5, 6 }, { 1, 1, 2, 1, 2, 2, 1 }, { 1, 3, 2, 2, 3, 1 }, { 1, 5, 1, 1, 6, 4 } };
    Solution s;

    for (auto &testCase : testCases)
    {
        s.wiggleSort(testCase);
    }
}
