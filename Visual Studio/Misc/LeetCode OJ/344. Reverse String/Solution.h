#pragma once

class Solution
{
public:
    string reverseString(const string &s)
    {
        return string(s.crbegin(), s.crend());
    }
};
