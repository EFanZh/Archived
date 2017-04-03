#pragma once

class Solution
{
public:
    vector<int> getRow(int rowIndex)
    {
        ++rowIndex;

        vector<int> result(rowIndex);
        size_t middle = (rowIndex + 1) / 2;

        result.front() = 1;

        for (size_t i = 1; i < middle; ++i)
        {
            result[i] = static_cast<int>(result[i - 1] * (rowIndex - i) / i);
        }

        copy(result.cbegin(), result.cbegin() + rowIndex / 2, result.rbegin());

        return result;
    }
};
