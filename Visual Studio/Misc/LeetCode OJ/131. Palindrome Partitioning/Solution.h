#pragma once

class Solution
{
    static bool isPalindrome(const string &s, size_t i, size_t j)
    {
        while (i < j)
        {
            if (s[i] != s[j])
            {
                return false;
            }

            ++i;
            --j;
        }

        return true;
    }

    static vector<vector<string>> partitionHelper(const string &s, size_t i)
    {
        if (i == s.length())
        {
            return { {} };
        }

        vector<vector<string>> result;

        for (size_t j = i; j < s.length(); ++j)
        {
            if (isPalindrome(s, i, j))
            {
                string current = s.substr(i, j - i + 1);
                auto newResult = partitionHelper(s, j + 1);

                for (auto &k : newResult)
                {
                    k.emplace(k.begin(), current);
                }

                move(newResult.begin(), newResult.end(), back_inserter(result));
            }
        }

        return result;
    }

public:
    vector<vector<string>> partition(string s)
    {
        return partitionHelper(s, 0);
    }
};
