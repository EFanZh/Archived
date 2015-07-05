#pragma once

static const pair<int, string> letters[] =
{
    { 1000, "M" },
    { 900, "CM" },
    { 500, "D" },
    { 400, "CD" },
    { 100, "C" },
    { 90, "XC" },
    { 50, "L" },
    { 40, "XL" },
    { 10, "X" },
    { 9, "IX" },
    { 5, "V" },
    { 4, "IV" },
    { 1, "I" }
};

class Solution
{
public:
    int romanToInt(string s)
    {
        int result = 0;
        size_t i = 0;

        for (const auto &letter : letters)
        {
            while (equal(s.cbegin() + i, s.cbegin() + (i + letter.second.length()), letter.second.cbegin()))
            {
                result += letter.first;
                i += letter.second.length();
            }
        }

        return result;
    }
};
