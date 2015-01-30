#pragma once

class Solution
{
    static string convertToTitleHelper(int n)
    {
        if (n < 26)
        {
            return string(1, static_cast<char>('A' + n));
        }
        else
        {
            return convertToTitleHelper(n / 26 - 1) + static_cast<char>('A' + n % 26);
        }
    }

public:
    string convertToTitle(int n)
    {
        return convertToTitleHelper(n - 1);
    }
};
