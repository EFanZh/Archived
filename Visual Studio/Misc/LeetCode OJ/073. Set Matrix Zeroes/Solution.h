#pragma once

class Solution
{
    static bool hasZero(const vector<int> &v)
    {
        return any_of(v.begin(), v.end(), [](int x) { return x == 0; });
    }

public:
    void setZeroes(vector<vector<int>> &matrix)
    {
        auto lastZeroRow = find_if(matrix.begin(), matrix.end(), hasZero);

        if (lastZeroRow == matrix.cend())
        {
            return;
        }

        auto it = find_if(lastZeroRow + 1, matrix.end(), hasZero);

        while (it != matrix.end())
        {
            for (size_t i = 0; i < lastZeroRow->size(); ++i)
            {
                if ((*lastZeroRow)[i] == 0)
                {
                    (*it)[i] = 0;
                }
            }

            fill(lastZeroRow->begin(), lastZeroRow->end(), 0);
            lastZeroRow = it;
            it = find_if(lastZeroRow + 1, matrix.end(), hasZero);
        }

        for (size_t i = 0; i < lastZeroRow->size(); ++i)
        {
            if ((*lastZeroRow)[i] == 0)
            {
                for (size_t j = 0; j < matrix.size(); ++j)
                {
                    matrix[j][i] = 0;
                }
            }
        }

        fill(lastZeroRow->begin(), lastZeroRow->end(), 0);
    }
};
