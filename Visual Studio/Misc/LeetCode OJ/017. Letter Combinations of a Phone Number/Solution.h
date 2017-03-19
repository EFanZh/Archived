#pragma once

static const string digitToChars[] = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

class Solution
{
public:
    vector<string> letterCombinations(string digits)
    {
        vector<string> result;

        if (digits.empty())
        {
            result.emplace_back("");
        }
        else
        {
            const auto &rest = letterCombinations(digits.substr(1));

            for (auto c : digitToChars[digits.front() - '0'])
            {
                for (auto &s : rest)
                {
                    result.emplace_back(c + s);
                }
            }
        }

        return result;
    }
};
