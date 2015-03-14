// https://leetcode.com/problems/valid-sudoku/

#include "Solution.h"

int main()
{
    array<const char *, 9> testCases[] = { { "....4.9..", "..21..3..", ".........", "........3", "...2.....", ".....7...", "...61....", "..9......", ".......9." },
                                           { "..4...63.", ".........", "5......9.", "...56....", "4.3.....1", "...7.....", "...5.....", ".........", "........." } };
    Solution s;

    for (const auto &testCase : testCases)
    {
        auto board = convertStringToMatrix(testCase);

        s.isValidSudoku(board);
    }
}
