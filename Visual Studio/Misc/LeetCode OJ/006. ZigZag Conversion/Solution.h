#pragma once

class Solution
{
public:
    string convert(string s, int nRows)
    {
        if (nRows == 1)
        {
            return s;
        }

        string result(s.size(), '\0');
        size_t skip = nRows * 2 - 2;
        size_t i = 0;

        for (size_t j = 0; j < s.length(); j += skip)
        {
            result[i++] = s[j];
        }

        for (int row = 1; row < nRows - 1; ++row)
        {
            for (size_t j = row; j < s.length(); j += skip)
            {
                result[i++] = s[j];

                size_t k = j + skip - row * 2;

                if (k < s.length())
                {
                    result[i++] = s[k];
                }
            }
        }

        for (size_t j = nRows - 1; j < s.length(); j += skip)
        {
            result[i++] = s[j];
        }

        return result;
    }
};
