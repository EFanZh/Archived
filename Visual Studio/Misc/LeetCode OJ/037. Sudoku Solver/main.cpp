// https://oj.leetcode.com/problems/sudoku-solver/

#include "Solution.h"

int main()
{
    initializer_list<const char *> testCases[] =
    {
        { "..9748...", "7........", ".2.1.9...", "..7...24.", ".64.1.59.", ".98...3..", "...8.3.2.", "........6", "...2759.." }
    };

    Solution solution;

    for (auto &testCase : testCases)
    {
        vector<vector<char>> board = convertStringToMatrix(testCase);

        solution.solveSudoku(board);
    }
}
