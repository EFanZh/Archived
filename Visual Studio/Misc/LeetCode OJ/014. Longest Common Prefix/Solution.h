#pragma once

class Solution
{
public:
    string longestCommonPrefix(vector<string> &strs)
    {
        if (strs.size() == 0)
        {
            return {};
        }
        else if (strs.size() == 1)
        {
            return strs.front();
        }

        const auto p = minmax_element(strs.cbegin(), strs.cend());
        size_t maxLength = min(p.first->length(), p.second->length());
        size_t length = 0;

        while (length < maxLength && (*p.first)[length] == (*p.second)[length])
        {
            ++length;
        }

        return p.first->substr(0, length);
    }
};
