#pragma once

static const pair<int, const char *> letters[] = { { 1000, "M" }, { 900, "CM" }, { 500, "D" }, { 400, "CD" },
                                                   { 100, "C" },  { 90, "XC" },  { 50, "L" },  { 40, "XL" },
                                                   { 10, "X" },   { 9, "IX" },   { 5, "V" },   { 4, "IV" },
                                                   { 1, "I" } };

class Solution
{
public:
    string intToRoman(int num)
    {
        string result;

        for (size_t i = 0; num > 0; ++i)
        {
            int n = num / letters[i].first;

            for (int j = 0; j < n; ++j)
            {
                result += letters[i].second;
            }

            num %= letters[i].first;
        }

        return result;
    }
};
