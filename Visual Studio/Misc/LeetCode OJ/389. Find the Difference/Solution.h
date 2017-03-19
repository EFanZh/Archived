#pragma once

class Solution
{
public:
    char findTheDifference(const string &s, const string &t)
    {
        const auto length = s.length();
        auto result = static_cast<unsigned char>(0);

        for (auto i = size_t(0); i < length; ++i)
        {
            result -= static_cast<unsigned char>(s[i]);
            result += static_cast<unsigned char>(t[i]);
        }

        return static_cast<char>(result + static_cast<unsigned char>(t.back()));
    }
};
