#pragma once

class Solution
{
public:
    void reverseWords(string &s)
    {
        stringstream ss(s);
        vector<string> strs;

        while (!ss.eof())
        {
            string word;
            ss >> word;

            if (!word.empty())
            {
                strs.emplace_back(word);
            }
        }

        if (strs.empty())
        {
            s.clear();
            return;
        }
        else
        {
            s = strs.back();
            for (auto it = strs.crbegin() + 1; it != strs.crend(); ++it)
            {
                s += ' ';
                s += *it;
            }
        }
    }
};
